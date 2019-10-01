using System;
using System.Collections.Generic;
using System.Text;
using Orcabot.Types.Enums.Ranks;
namespace Orcabot.Api.Types.EDSM.Commander
{

    #region Ranks
    public class JSONRanks : IApiResponse
    {
        public int? msgnum;
        public string msg;
        public JSONRanksGeneric<int> ranks;
        public JSONRanksGeneric<int> progress;
        public JSONRanksGeneric<string> ranksVerbose;

    }
    public class JSONRanksGeneric<T> 
    {
        public T Combat, Trade, Explore, CQC, Federation, Empire;
    }
    namespace Helper
    {
        public static class JSONRanksHelper
        {
            public static Ranks Convert(this Ranks c) {
                throw new NotImplementedException(); //TODO
            }
        }
    }
    public class Ranks
    {
        public Combat Combat;
        public Trade Trade;
        public Explore Explore;
        public Federation Federation;
        public Empire Imperial;
    }

    #endregion
    #region Credits
    public class JSONCredits :IApiResponse
    {
        public int? msgnum;
        public string msg;
        public JSONCreditsValue[] credits;
    }
    public class JSONCreditsValue
    {
        public long? balance, loan;
        public string date;
    }

    namespace Helper {
        public static class JSONCreditsHelper
        {
            public static Credits Convert(this JSONCredits commanderJSONCredits) {
                throw new NotImplementedException(); //TODO
            }
        }
    }
    public class Credits
    {
        //TODO
    }
    #endregion
    #region Materials
    public class JSONMaterials :IApiResponse
    {
        public int? msgnum;
        public string msg;
        public JSONMaterialsEntry[] materials;
        public JSONMaterialsEntry[] data;
        public JSONMaterialsEntry[] cargo;

    }
    public class JSONMaterialsEntry
    {
        public string type, name;
        public int? qty;
    }
    namespace Helper
    {
        public static class JSONMaterialsHelper
        {
            public static Materials Convert(this JSONMaterials commanderJSONMaterials) {
                throw new NotImplementedException(); //TODO
            }
        }
    }
    public class Materials
    {
        //TODO
    }
    #endregion

}
