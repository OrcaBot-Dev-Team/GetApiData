using System;
using System.Collections.Generic;
using System.Text;

namespace Orcabot.Api.Types.EDSM.Commander
{

    #region Ranks
    public class JSONRanks
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
            public static CommanderRanks Convert(this CommanderRanks c) {
                throw new NotImplementedException(); //TODO
            }
        }
    }
    public class CommanderRanks
    {
        //TODO
    }
    #endregion
    #region Credits
    public class JSONCredits
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
            public static CommanderCredits Convert(this JSONCredits commanderJSONCredits) {
                throw new NotImplementedException(); //TODO
            }
        }
    }
    public class CommanderCredits
    {
        //TODO
    }
    #endregion
    #region Materials
    public class JSONMaterials
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
            public static CommanderMaterials Convert(this JSONMaterials commanderJSONMaterials) {
                throw new NotImplementedException(); //TODO
            }
        }
    }
    public class CommanderMaterials
    {
        //TODO
    }
    #endregion

}
