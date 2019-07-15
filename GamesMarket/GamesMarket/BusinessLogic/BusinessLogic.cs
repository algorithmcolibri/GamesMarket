using GamesMarket.Models.DBModel;
using GamesMarket.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GamesMarket.BusinessLogic
{
    public class BusinessLogic
    {
		#region Fields

        static string connectionString = @"Data Source = 128.0.175.59; Initial Catalog = GameDev_Company; Integrated Security = False; Persist Security Info=False;User ID = GameDev; Password=GameDev123456";


        #endregion

        #region Methods

        #region Insert

        public static bool InsertTypeGame(Models.BLModel.TypeGame model, out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(model.NameJanr))
            {
                message = "Type game is empty";
                return false;
            }
            Models.DBModel.TypeGame typeGameDB = new Models.DBModel.TypeGame
            {
                NameJanr = model.NameJanr
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.InsertTypeGame(typeGameDB);
            if (!result)
            {
                message = "Type game is not added";
            }
            return true;
        }


        public static bool InsertOS(Models.BLModel.OS model, out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(model.Name))
            {
                message = "Operation system is empty";
                return false;
            }
            Models.DBModel.OS oS = new Models.DBModel.OS
            {
                Name = model.Name,
                VersionOS = model.VersionOS
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.InsertOs(oS);
            if (!result)
            {
                message = "Operation system  is not added";
            }
            return true;
        }

        public static bool InsertRam(Models.BLModel.Ram model, out string message)
        {
            message = null;
            if (model.SizeRam == 0)
            {
                message = "Ram is empty";
                return false;
            }
            Models.DBModel.Ram ram = new Models.DBModel.Ram
            {
                SizeRam = model.SizeRam
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.InsertRam(ram);
            if (!result)
            {
                message = "Ram  is not added";
            }
            return true;
        }

        public static bool InsertCpu(Models.BLModel.Cpu model, out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(model.Model))
            {
                message = "Cpu is empty";
                return false;
            }
            Models.DBModel.Cpu cpu = new Models.DBModel.Cpu
            {
                Model = model.Model
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.InsertCpu(cpu);
            if (!result)
            {
                message = "Cpu  is not added";
            }
            return true;
        }


        public static bool InsertVideoCard(Models.BLModel.VideoCard model, out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(model.Model))
            {
                message = "Video Card is empty";
                return false;
            }
            Models.DBModel.VideoCard video = new Models.DBModel.VideoCard
            {
                Model = model.Model,
                SizeVC = model.SizeVC
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.InsertVideoCard(video);
            if (!result)
            {
                message = "Video Card  is not added";
            }
            return true;
        }

        public static bool InsertGameCatalog(Models.BLModel.GameCatalog model, out string message)
        {
            message = null;
            if (string.IsNullOrEmpty(model.Name))
            {
                message = "Game Catalog is empty";
                return false;
            }
            Models.DBModel.GameCatalog video = new Models.DBModel.GameCatalog
            {
                Name = model.Name,
                Price = model.Price,
                DescribeGame = model.DescribeGame,
                TypeGame = model.TypeGame,
                Url = model.Url,
                Key = model.Key
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.InsertGameCatalog(video);
            if (!result)
            {
                message = " Game Catalog  is not added";
            }
            return true;
        }

        public static bool InsertConfigCatalog(Models.BLModel.ConfigCatalog model, out string message)
        {
            message = null;
            if (model.CPU == 0)
            {
                message = " Config Catalog is empty";
                return false;
            }
            Models.DBModel.ConfigCatalog config = new Models.DBModel.ConfigCatalog
            {
                CPU = model.CPU,
                RAM = model.RAM,
                VideoCard = model.VideoCard,
                OS = model.OS,
                SizeOnDisc = model.SizeOnDisc,
                GameId = model.GameId
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.InsertConfigCatalog(config);
            if (!result)
            {
                message = " Config Catalog  is not added";
            }
            return true;
        }

        public static bool InsertCompConfig(Models.BLModel.CompConfig model, out string message)
        {
            message = null;
            if (model.CPU == 0)
            {
                message = "Comp Config is empty";
                return false;
            }
            Models.DBModel.CompConfig config = new Models.DBModel.CompConfig
            {
                CPU = model.CPU,
                RAM = model.RAM,
                VideoCard = model.VideoCard,
                OS = model.OS
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.InsertCompConfig(config);
            if (!result)
            {
                message = "Comp Config  is not added";
            }
            return true;
        }
        #endregion

        #region Select
        public static IList<Models.BLModel.CompConfig> SelectCompConfig(string idUser = null)
        {
            var model = new DBLogic(connectionString);

            return model.SelectCompConfig(idUser);
        }

        public static IList<Models.BLModel.ConfigCatalog> SelectConfigCatalog(int gameId = default(int))
        {
            var model = new DBLogic(connectionString);

            return model.SelectConfigCatalog(gameId);
        }

        public static IList<Models.BLModel.GameCatalog> SelectGameCatalog()
        {
            var model = new DBLogic(connectionString);

            return model.SelectGameCatalog();
        }

        public static IList<Models.BLModel.Wallet> SelectWallet(string idUser = null)
        {
            var model = new DBLogic(connectionString);

            return model.SelectWallet(idUser);
        }

        public static IList<Models.BLModel.VideoCard> SelectVideoCardList()
        {
            var model = new DBLogic(connectionString);

            return model.SelectVideoCard();
        }

        public static IList<Models.BLModel.Cpu> SelectCpuList()
        {
            var model = new DBLogic(connectionString);

            return model.SelectCpu();
        }

        public static IList<Models.BLModel.Ram> SelectRamList()
        {
            var model = new DBLogic(connectionString);

            return model.SelectRam();
        }

        public static IList<Models.BLModel.OS> SelectOSList()
        {
            var model = new DBLogic(connectionString);

            return model.SelectOS();
        }

        public static IList<Models.BLModel.TypeGame> SelectTypeGame()
        {
            //var model = new DBLogic(connectionString);

            return DBLogic.SelectTypeGame();
        }

        #endregion

        #region Delete

        public static bool DeleteTypeGame(Models.BLModel.TypeGame model, out string message)
        {
            message = null;
            if (model.Id == 0)
            {
                message = "Choose record";
                return false;
            }
            Models.DBModel.TypeGame typeGame = new Models.DBModel.TypeGame
            {
                NameJanr = model.NameJanr,
                Id = model.Id
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.DeleteTypeGame(typeGame);
            if (!result)
            {
                message = "Delete 0 rows";
                return false;
            }
            return true;

        }

        public static bool DeleteOs(Models.BLModel.OS model, out string message)
        {
            message = null;
            if (model.Id == 0)
            {
                message = "Choose record";
                return false;
            }
            Models.DBModel.OS os = new Models.DBModel.OS
            {
                Name = model.Name,
                Id = model.Id
            };
            DBLogic dBLogic = new DBLogic(connectionString);

            var result = dBLogic.DeleteOs(os);
            if (!result)
            {
                message = "Delete 0 rows";
                return false;
            }
            return true;

        }


       
        #endregion

        #region Update
        //public static bool UpdateTypeGame(Models.BLModel.TypeGame model, out string message)
        //{
        //    message = null;
        //    if (model.Id != 0)
        //    {
        //        message = "Choose record";
        //        return false;
        //    }
        //    Models.DBModel.TypeGame typeGame = new Models.DBModel.TypeGame
        //    {
        //        NameJanr = model.NameJanr,
        //        Id = model.Id
        //    };
        //    DBLogic dBLogic = new DBLogic(connectionString);

        //    var result = dBLogic.UpdateTypeGame(typeGame);
        //    if (!result)
        //    {
        //        message = "Update failed";
        //        return false;
        //    }
        //    return true;

        //}
        #endregion

        #endregion
    }
}