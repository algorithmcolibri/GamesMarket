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

        static string connectionString = @"Data Source = ; Initial Catalog = ; Integrated Security = False; Persist Security Info=False; User ID = ; Password= ";// todo Add DB Config


        #endregion

        #region Methods

        #region Insert

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
            var model = new DBLogic(connectionString);

            return model.SelectTypeGame();
        }

        #endregion

        #region Delete

        #endregion

        #region Update

        #endregion

        #endregion
    }
}