using log4net;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HL7SenderWinForm2015
{
    public static class Parse
    {
        private static readonly ILog _log = log4net.LogManager.GetLogger("RollingFileAppender");
        public static string removeNKwDTM(string message)
        {
            string retmessage = message;
            
            try
            {
                retmessage = filterSegment(message);
            }

            catch (Exception e)
            {
                MessageBox.Show("Charecter removing exception " + e.Message);
            }
            return retmessage;
        }
        private static string filterSegment(string msg)
        {
            try
            {
                _log.Info("Start Filtering the segments.");

                IEnumerable<string> segmentsToFilter = ADTSegmentsToFilter;
                List<string> filteredSegments;
                List<string> Segments;
                string structureName = null;
                string msgToParse = msg;
                msgToParse = removeSegments(msgToParse, segmentsToFilter, out Segments);
                if (msgToParse != null)
                {
                    PipeParser pp = new PipeParser();
                    IMessage imsg = pp.Parse(msgToParse);

                    structureName = imsg.GetStructureName();
                }

                //Check whether HL7 received is GS ADT message.
                if (GSADTMessageType.Contains(structureName) || msg.Contains(ADT_60))
                {
                    //IAM segment doesn't get filtered out in ADT_A60
                    if (msg.Contains(ADT_60))
                    {
                        segmentsToFilter = segmentsToFilter.Where(z => z != "IAM|");
                    }

                    msg = removeSegments(msg, segmentsToFilter, out filteredSegments);

                    if (filteredSegments.Count > 0)
                    {
                        foreach (string seg in filteredSegments)
                        {
                            _log.InfoFormat("# Filtered Segment : {seg}", seg);
                        }
                    }
                    else
                    {
                        _log.Info("# No Segments to filter");
                    }
                }
                _log.Info("# End Filtering Segments");
            }

            catch (Exception ex)
            {
                _log.Error("# Exception {ex}" , ex);
            }
            return msg;

        }

        private static string removeSegments(string msg, IEnumerable<string> segmentsToFilter, out List<string> filteredSegments)
        {
            filteredSegments = new List<string>();

            foreach (var seg in segmentsToFilter.Where(x => msg.Contains(CR + x)))
            {
                string[] lines = msg.Split(new string[] { CR.ToString() }, StringSplitOptions.None).ToArray();
                var filteredLines = lines.Where(x => x.Length > 3 && !x.Substring(0, 4).Contains(seg));
                msg = string.Join(CR.ToString(), filteredLines.ToArray());
                filteredSegments.Add(seg);
            }
            return msg;
        }

        private static readonly ReadOnlyCollection<string> ADTSegmentsToFilter = new ReadOnlyCollection<string>(new[]
        {
            "PD1|",
            "NTE|",
            "NK1|",
            "PV2|",
            "ZPV|",
            "ZBE|",
            "ROL|",
            "OBX|",
            "DG1|",
            "ACC|",
            "ZPE|",
            "IAM|",
            "ZPD|",
        });

        private static readonly ReadOnlyCollection<string> GSADTMessageType = new ReadOnlyCollection<string>(new[]
{
            "ADT_A01",
            "ADT_A02",
            "ADT_A03",
            "ADT_A06",
            "ADT_A07",
            "ADT_A08",
            "ADT_A09",
            "ADT_A10",
            "ADT_A11",
            "ADT_A12",
            "ADT_A13",
            "ADT_A31",
            "ADT_A40"
        });
        private const char CR = (char)13;
        private const string MSG_CANCEL_ADMISSION = "ADT_A11";
        private const string MSG_FOOD_ALLERGY = "ADT_A60";
        private const string ADT_60 = "ADT^A60";
    }
    public class AdtMessage
    {
        public IMessage Msg { get; set; }
        public long MsgSequence { get; set; }
        public long MessageLogId { get; set; }
        public string Ms { get; set; }

    }
}
