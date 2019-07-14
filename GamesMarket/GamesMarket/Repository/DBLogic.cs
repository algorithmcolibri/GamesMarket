using GamesMarket.Models.DBModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace GamesMarket.Repository
{
    public class DBLogic
    {
        #region Fields

        static string connectionString = @"Data Source = 128.0.175.59; Initial Catalog = GameDev_Company; Integrated Security = False; Persist Security Info=False;User ID = GameDev; Password=GameDev123456";

        #endregion

        #region Ctor

        public DBLogic(string conn)
        {
            connectionString = conn;
        }

		#endregion

		#region Methods

		#region Insert

		public bool CreateOrder(List<Basket> baskets)
		{
			if(baskets.Count == 0)
			{
				return false;
			}

			string idUser = baskets[0].UserId;
			double totalPrice = baskets.Sum(item => item.Price);

			string sql = "select * from Wallet where IDUser = @id and Balance >= @balance";

			using (IDbConnection db = new SqlConnection(connectionString))
			{
				IEnumerable<Basket> models = db.Query<Basket>(sql, param: new { id = idUser, balance = totalPrice});

				if (models.Count(item => item.UserId == idUser) == 0)
				{
					return false;
				}

				sql = "insert into Orders (IdUser, TotalPrice)" +
					"values (@idUser, @totalPrice)";
				db.Execute(sql, param: new{	idUser, totalPrice	});
				sql = "select max(IDOrder) from Orders";
				var currentIdOrder = db.Query<int>(sql);
				foreach(var item in baskets)
				{
					sql = "insert into OrderGame(iDOrder,GameId) values(@currentIdOrder, @gameId)";
					db.Execute(sql, param: new { currentIdOrder, gameId = item.GameId });
				}
				LostMoneyOnWallet(idUser, totalPrice);
			}
			return true;
		}


		public bool InsertTypeGame(TypeGame model)
        {
            string nameJanr = model.NameJanr;
            string sql = "select nameJanr from TypeGame ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<TypeGame> models = db.Query<TypeGame>(sql);

                if (models.Count(item => item.NameJanr == nameJanr) > 0)
                {
                    return false;
                }

                sql = "insert into TypeGame (NameJanr)" +
                    "values (@nameJanr)";
                db.Execute(sql, new
                {
                    model.NameJanr
                });

            }
            return true;
        }

		public static void InsertGameToBasket(Basket basket)
		{
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var sql = "insert into Basket (IDUSer, GameId, Price)" +
					"values (@basketModel)";
				db.Execute(sql, new { basketModel = basket });
			}
		}

		public static void InsertWalletToNewUser(string id)
		{
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var sql = "insert into Wallet (IDUSer, Balance, Bank)" +
					"values (@userId,0,'PrivatBank')";
				db.Execute(sql, param: new {userId = id });
			}
		}

		public static void InsertBasketToNewUser(string id)
		{
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var sql = "insert into Basket (IDUSer)" +
					"values (@Id)";
				db.Execute(sql, param: new { id });
			}
		}

		public bool InsertCpu(Cpu model)
        {
            string modelCpu = model.Model;
            string sql = "select Model from Cpu ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<Cpu> models = db.Query<Cpu>(sql);

                if (models.Count(item => item.Model == modelCpu) > 0)
                {
                    return false;
                }

                sql = "insert into Cpu (Model)" +
                    "values (@Model)";
                db.Execute(sql, new
                {
                    model.Model
                });

            }
            return true;
        }

        public bool InsertRam(Ram model)
        {
            decimal size = model.SizeRam;
            string sql = "select SizeRam from Ram ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<Ram> models = db.Query<Ram>(sql);

                if (models.Count(item => item.SizeRam == size) > 0)
                {
                    return false;
                }

                sql = "insert into Ram (SizeRam)" +
                    "values (@SizeRam)";
                db.Execute(sql, new
                {
                    model.SizeRam
                });

            }
            return true;
        }

        public bool InsertOs(OS model)
        {
            string name = model.Name;
            string sql = "select Name from OS ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<OS> models = db.Query<OS>(sql);

                if (models.Count(item => item.Name == name) > 0)
                {
                    return false;
                }

                sql = "insert into OS (Name, VersionOS)" +
                    "values (@name, @VersionOS)";
                db.Execute(sql, new
                {
                    model.Name,
                    model.VersionOS
                });

            }
            return true;
        }

        public bool InsertVideoCard(VideoCard model)
        {
            string modelVC = model.Model;
            string sql = "select Model from VideoCard ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<VideoCard> models = db.Query<VideoCard>(sql);

                if (models.Count(item => item.Model == modelVC) > 0)
                {
                    return false;
                }

                sql = "insert into VideoCard (Model, SizeVC)" +
                    "values (@Model, @SizeVC)";
                db.Execute(sql, new
                {
                    model.Model,
                    model.SizeVC
                });

            }
            return true;
        }

        public bool InsertGameCatalog(GameCatalog model)
        {
            string name = model.Name;
            string sql = "select Name from GameCatalog ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<GameCatalog> models = db.Query<GameCatalog>(sql);

                if (models.Count(item => item.Name == name) > 0)
                {
                    return false;
                }

                sql = "insert into GameCatalog (Name, Price, DescribeGame, TypeGame)" +
                    "values (@Name, @Price, @DescribeGame,@TypeGame)";
                db.Execute(sql, new
                {
                    model.Name,
                    model.Price,
                    model.DescribeGame,
                    model.TypeGame
                });

            }
            return true;
        }

        public bool InsertCompConfig(CompConfig model)
        {

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sql = "insert into CompConfig (CPU, OS, RAM, VideoCard)" +
                   "values (@CPU, @OS, @RAM,@VideoCard)";
                IEnumerable<CompConfig> models = db.Query<CompConfig>(sql);

                db.Execute(sql, new
                {
                    model.CPU,
                    model.OS,
                    model.RAM,
                    model.VideoCard
                });

            }
            return true;
        }

        public bool InsertConfigCatalog(ConfigCatalog model)
        {

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sql = "insert into ConfigCatalog (CPU, OS, RAM, VideoCard, SizeOnDisc,GameId)" +
                   "values (@CPU, @OS, @RAM,@VideoCard, @SizeOnDisc, @GameId)";
                IEnumerable<ConfigCatalog> models = db.Query<ConfigCatalog>(sql);

                db.Execute(sql, new
                {
                    model.CPU,
                    model.OS,
                    model.RAM,
                    model.VideoCard,
                    model.SizeOnDisc,
                    model.GameId
                });

            }
            return true;
        }
        #endregion

        #region Select
        public IList<Models.BLModel.Ram> SelectRam(int id = default(int)) 
        {
            var query = "SELECT * FROM Ram WHERE 1=1";
            if (id != default(int))
            {
                query += string.Format(" AND ID = {0}", id);
            }
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var ram = new List<Models.BLModel.Ram>();

                var model = db.Query<Ram>(query).ToList();

                foreach (var mod in model)
                {
                    ram.Add(new Models.BLModel.Ram
                    {
                        Id = mod.Id,
                        SizeRam = mod.SizeRam
                    });
                }
                return ram;
            }
        }

        public IList<Models.BLModel.Cpu> SelectCpu(int id = default(int)) 
        {
            var query = "SELECT * FROM Cpu WHERE 1=1";
            if (id != default(int))
            {
                query += string.Format(" AND ID = {0}", id);
            }
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var cpu = new List<Models.BLModel.Cpu>();

                var model = db.Query<Cpu>(query).ToList();

                foreach (var mod in model)
                {
                    cpu.Add(new Models.BLModel.Cpu
                    {
                        Id = mod.Id,
                        Model = mod.Model
                    });
                }
                return cpu;
            }
        }

		public static IList<Models.BLModel.Basket> SelectBasket(string userId)
		{
			var sql = "select * from Basket where IDUser = @userId";
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var result = db.Query<Models.BLModel.Basket>(sql, param: new { userId}).ToList();
				foreach(var item in result)
				{
					sql = "select Name from GameCatalog where ID = @gameId";
					var name = db.Query<string>(sql, param: new { gameId = item.GameId }).FirstOrDefault();
					item.GameName = name;
				}
				return result;
			}
		}

        public IList<Models.BLModel.VideoCard> SelectVideoCard(int id = default(int)) 
        {
            var query = "SELECT * FROM VideoCard WHERE 1=1";
            if (id != default(int))
            {
                query += string.Format(" AND ID = {0}", id);
            }
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var videoCard = new List<Models.BLModel.VideoCard>();

                var model = db.Query<VideoCard>(query).ToList();

                foreach (var mod in model)
                {
                    videoCard.Add(new Models.BLModel.VideoCard
                    {
                        Id = mod.Id,
                        Model = mod.Model,
                        SizeVC = mod.SizeVC
                    });
                }
                return videoCard;
            }
        }
        public IList<Models.BLModel.OS> SelectOS(int id = default(int)) 
        {
            var query = "SELECT * FROM OS WHERE 1=1";
            if (id != default(int))
            {
                query += string.Format(" AND ID = {0}", id);
            }
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var os = new List<Models.BLModel.OS>();

                var model = db.Query<OS>(query).ToList();

                foreach (var mod in model)
                {
                    os.Add(new Models.BLModel.OS
                    {
                        Id = mod.Id,
                        Name = mod.Name,
                        VersionOS = mod.VersionOS
                    });
                }
                return os;
            }
        }

        public IList<Models.BLModel.ConfigCatalog> SelectConfigCatalog(int gameId = default(int)) 
        {
            var query = "SELECT * FROM ConfigCatalog WHERE 1=1";
            if (gameId != default(int))
            {
                query += string.Format(" AND GameId = {0}", gameId);
            }
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var configCatalog = new List<Models.BLModel.ConfigCatalog>();
                var ramList = SelectRam();
                var cpuList = SelectCpu();
                var osList = SelectOS();
                var videoCardList = SelectVideoCard();
                var model = db.Query<ConfigCatalog>(query).ToList();

                foreach (var mod in model)
                {
                    configCatalog.Add(new Models.BLModel.ConfigCatalog
                    {
                        Id = mod.Id,
                        CPU = mod.CPU,
                        OS = mod.OS,
                        RAM = mod.RAM,
                        VideoCard = mod.VideoCard,
                        SizeOnDisc = mod.SizeOnDisc,
                        SizeRam = ramList.Where(w=>w.Id == mod.RAM).Select(s => s.SizeRam).FirstOrDefault(),
                        Model = cpuList.Where(w=>w.Id == mod.CPU).Select(s => s.Model).FirstOrDefault(),
                        Name = osList.Where(w=>w.Id == mod.OS).Select(s => s.Name).FirstOrDefault(),
                        VersionOS = osList.Where(w=>w.Id == mod.OS).Select(s => s.VersionOS).FirstOrDefault(),
                        ModelVC = videoCardList.Where(w=>w.Id == mod.VideoCard).Select(s => s.Model).FirstOrDefault(),
                        SizeVC = videoCardList.Where(w => w.Id == mod.VideoCard).Select(s => s.SizeVC).FirstOrDefault(),
                    });
                }
                return configCatalog;
            }
        }

        public IList<Models.BLModel.GameCatalog> SelectGameCatalog()
        {
            var query = "SELECT * FROM GameCatalog";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var gameCatalog = new List<Models.BLModel.GameCatalog>();
                var typeGameList = SelectTypeGame();
                var model = db.Query<GameCatalog>(query).ToList();

                foreach (var mod in model)
                {
                    gameCatalog.Add(new Models.BLModel.GameCatalog
                    {
                        Id = mod.Id,
                        DescribeGame = mod.DescribeGame,
                        Name = mod.Name,
                        Price = mod.Price,
                        TypeGame = mod.TypeGame,
                        NameJanr = typeGameList.Where(w=>w.Id == mod.TypeGame).Select(s=>s.NameJanr).FirstOrDefault()
                    });
                }
                return gameCatalog;
            }
        }

        public IList<Models.BLModel.CompConfig> SelectCompConfig(string idUser = null)
        {
            var query = "SELECT * FROM CompConfig WHERE 1=1";
            if (!string.IsNullOrEmpty(idUser))
            {
                query += string.Format(" AND IDUSER = '{0}'", idUser);
            }
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var compConfig = new List<Models.BLModel.CompConfig>();

                var ramList = SelectRam();
                var cpuList = SelectCpu();
                var osList = SelectOS();
                var videoCardList = SelectVideoCard();

                var model = db.Query<CompConfig>(query).ToList();

                foreach (var mod in model)
                {
                    compConfig.Add(new Models.BLModel.CompConfig
                    {
                        Id = mod.Id,
                        CPU = mod.CPU,
                        OS = mod.OS,
                        RAM = mod.RAM,                
                        VideoCard = mod.VideoCard,
                        SizeRam = ramList.Where(w => w.Id == mod.RAM).Select(s => s.SizeRam).FirstOrDefault(),
                        Model = cpuList.Where(w => w.Id == mod.CPU).Select(s => s.Model).FirstOrDefault(),
                        Name = osList.Where(w => w.Id == mod.OS).Select(s => s.Name).FirstOrDefault(),
                        VersionOS = osList.Where(w => w.Id == mod.OS).Select(s => s.VersionOS).FirstOrDefault(),
                        ModelVC = videoCardList.Where(w => w.Id == mod.VideoCard).Select(s => s.Model).FirstOrDefault(),
                        SizeVC = videoCardList.Where(w => w.Id == mod.VideoCard).Select(s => s.SizeVC).FirstOrDefault()
                    });
                }
                return compConfig;
            }
        }

        public IList<Models.BLModel.Wallet> SelectWallet(string idUser = null) 
        {
            var query = "SELECT * FROM Wallet WHERE 1=1";
            if (!string.IsNullOrEmpty(idUser))
            {
                query += string.Format(" AND IDUSER = '{0}'", idUser);
            }
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var wallet = new List<Models.BLModel.Wallet>();

                var model = db.Query<Wallet>(query).ToList();

                foreach (var mod in model)
                {
                    wallet.Add(new Models.BLModel.Wallet
                    {
                        Id = mod.Id,
                        Balance = mod.Balance,
                        Bank = mod.Bank
                    });
                }
                return wallet;
            }
        }

        public IList<Models.BLModel.TypeGame> SelectTypeGame()
        {
            string sqlQuery = "SELECT ID, NAMEJANR  FROM TYPEGAME";


            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var typeGame = new List<Models.BLModel.TypeGame>();

                var model = db.Query<TypeGame>(sqlQuery).ToList();
                foreach (var mod in model)
                {
                    typeGame.Add(new Models.BLModel.TypeGame
                    {
                        Id = mod.Id,
                        NameJanr = mod.NameJanr
                    });
                }

                return typeGame;
            }
        }
		#endregion

		#region Delete


		public static void DeleteGameFromoBasket(string userId,int gameId)
		{
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var sql = "delete from Basket where UserId = @userId and GameId = @gameId" +
					"values (@basketModel)";
				db.Execute(sql, new { userId, gameId });
			}
		}

		public bool DeleteTypeGame(TypeGame model)
        {
            string nameJanr = model.NameJanr;
            string sql = "select nameJanr from TypeGame ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<TypeGame> models = db.Query<TypeGame>(sql);

                if (models.Count(item => item.NameJanr == nameJanr) > 0)
                {
                    return false;
                }

                sql = "DELETE FROM TypeGame WHERE ID = @Id";
                db.Execute(sql, new
                {
                    model.Id
                });

            }
            return true;
        }

        public bool DeleteRam(Ram model)
        {
            decimal size = model.SizeRam;
            string sql = "select ID from RAM ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<Ram> models = db.Query<Ram>(sql);

                if (models.Count(item => item.SizeRam == size) > 0)
                {
                    return false;
                }

                sql = "DELETE FROM RAM WHERE ID = @Id";
                db.Execute(sql, new
                {
                    model.Id
                });

            }
            return true;
        }

        public bool DeleteOs(OS model)
        {
            string osVersion = model.VersionOS;
            string sql = "select ID from OS ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<OS> models = db.Query<OS>(sql);

                if (models.Count(item => item.VersionOS == osVersion) > 0)
                {
                    return false;
                }

                sql = "DELETE FROM OS WHERE ID = @Id";
                db.Execute(sql, new
                {
                    model.Id
                });

            }
            return true;
        }


        public bool DeleteVideoCard(VideoCard model)
        {
            string modelCard = model.Model;
            string sql = "select ID from VIDEOCARD ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<VideoCard> models = db.Query<VideoCard>(sql);

                if (models.Count(item => item.Model == modelCard) > 0)
                {
                    return false;
                }

                sql = "DELETE FROM VIDEOCARD WHERE ID = @Id";
                db.Execute(sql, new
                {
                    model.Id
                });

            }
            return true;
        }

        public bool DeleteCpu(Cpu model)
        {
            string modelCpu = model.Model;
            string sql = "select ID from Cpu ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<Cpu> models = db.Query<Cpu>(sql);

                if (models.Count(item => item.Model == modelCpu) > 0)
                {
                    return false;
                }

                sql = "DELETE FROM Cpu WHERE ID = @Id";
                db.Execute(sql, new
                {
                    model.Id
                });

            }
            return true;
        }

        public bool DeleteGameCatalog(GameCatalog model)
        {
            string name = model.Name;
            string sql = "select ID from GameCatalog ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<GameCatalog> models = db.Query<GameCatalog>(sql);

                if (models.Count(item => item.Name == name) > 0)
                {
                    return false;
                }

                sql = "DELETE FROM GameCatalog WHERE ID = @Id";
                db.Execute(sql, new
                {
                    model.Id
                });

            }
            return true;
        }

        public bool DeleteConfigCatalog(ConfigCatalog model)
        {
            int id = model.Id;
            string sql = "select ID from ConfigCatalog ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<ConfigCatalog> models = db.Query<ConfigCatalog>(sql);

                if (models.Count(item => item.Id == id) > 0)
                {
                    return false;
                }

                sql = "DELETE FROM ConfigCatalog WHERE ID = @Id";
                db.Execute(sql, new
                {
                    model.Id
                });

            }
            return true;
        }

        public bool DeleteCompConfig(CompConfig model)
        {
            int id = model.Id;
            string sql = "select ID from CompConfig ";
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                IEnumerable<CompConfig> models = db.Query<CompConfig>(sql);

                if (models.Count(item => item.Id == id) > 0)
                {
                    return false;
                }

                sql = "DELETE FROM CompConfig WHERE ID = @Id";
                db.Execute(sql, new
                {
                    model.Id
                });

            }
            return true;
        }

        #endregion

        #region Update
		
		public static void AddMoneyOnWallet(string id, double summ)
		{
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var sql = "update Wallet set Balance = Balance + @summ" +
					"where IDUser=@id";
				db.Execute(sql, param: new { id, summ });
			}
		}

		public static void LostMoneyOnWallet(string id, double summ)
		{
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var sql = "update Wallet set Balance = Balance - @summ" +
					"where IDUser=@id";
				db.Execute(sql, param: new { id, summ });
			}
		}

		public static void AddGameToBasket(string id, double summ)
		{
			using (IDbConnection db = new SqlConnection(connectionString))
			{
				var sql = "update Basket set Balance = Balance - @summ" +
					"where IDUser=@id";
				db.Execute(sql, param: new { id, summ });
			}
		}
		#endregion

		#endregion
	}
}