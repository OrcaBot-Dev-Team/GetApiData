using System;
using System.Collections.Generic;
using System.Text;
using Orcabot.Types.Enums.Ranks;
using Orcabot.Api.Types;
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
            public static EDSMResponse<Ranks> Convert(this JSONRanks c,int? htmlStatus, string htmlStatusMessage) {
                Ranks ranks = ranks = ParseRanks();

                return new EDSMResponse<Ranks> {
                    Message = c.msg,
                    MessageNumber = c.msgnum ?? -1,
                    Data = ranks,
                    HTMLStatus = htmlStatus ?? -1,
                    HTMLStatusResponse = htmlStatusMessage
                };

                Ranks ParseRanks(){
                    if (c == null)
                        return null;
                    return new Ranks {
                        Combat = (Combat)c.ranks.Combat,
                        Trade = (Trade)c.ranks.Trade,
                        Explore = (Explore)c.ranks.Explore,
                        Federation = (Federation)c.ranks.Federation,
                        Imperial = (Empire)c.ranks.Empire,
                        Progress = c.progress
                    };


                }
            }
        }
    }
    public class Ranks : IApiConverted
    {
        public Combat Combat;
        public Trade Trade;
        public Explore Explore;
        public Federation Federation;
        public Empire Imperial;

        public JSONRanksGeneric<int> Progress;
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
            public static EDSMResponse<Credits> Convert(this JSONCredits commanderJSONCredits) {
                throw new NotImplementedException();
            }
        }
    }
    public class Credits : IApiConverted
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
    public class Materials : IApiConverted
    {
        //TODO
    }
    #endregion

}
