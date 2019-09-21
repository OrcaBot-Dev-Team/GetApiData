using System;
using System.Collections.Generic;
using System.Text;

namespace Orcabot.Api.Types.EDSM
{

    #region Ranks
    public class CommanderJSONRanks
    {
        public int msgnum;
        public string msg;
        public CommanderJSONRanksGeneric<int> ranks;
        public CommanderJSONRanksGeneric<int> progress;
        public CommanderJSONRanksGeneric<string> ranksVerbose;

    }
    public class CommanderJSONRanksGeneric<T>
    {
        public T Combat, Trade, Explore, CQC, Federation, Empire;
    }
    namespace Helper
    {
        public static class CommanderJSONRanksHelper
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
    public class CommanderJSONCredits
    {
        public int msgnum;
        public string msg;
        public CommanderJSONCreditsValue[] credits;
    }
    public class CommanderJSONCreditsValue
    {
        public long balance, loan;
        public string date;
    }

    namespace Helper {
        public static class CommanderJSONCreditsHelper
        {
            public static CommanderCredits Convert(this CommanderJSONCredits commanderJSONCredits) {
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
    public class CommanderJSONMaterials
    {
        public int msgnum;
        public string msg;
        public CommanderJSONMaterialsEntry[] materials;
        public CommanderJSONMaterialsEntry[] data;
        public CommanderJSONMaterialsEntry[] cargo;

    }
    public class CommanderJSONMaterialsEntry
    {
        public string type, name;
        public int qty;
    }
    namespace Helper
    {
        public static class CommanderJSONMaterialsHelper
        {
            public static CommanderMaterials Convert(this CommanderJSONMaterials commanderJSONMaterials) {
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
