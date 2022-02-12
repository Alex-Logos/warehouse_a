using Com.ZimVie.Wcs.Framework;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ResultVo : ValueObject
    {

        /// <summary>
        /// get and set ResultId
        /// </summary>
        public int ResultId = 0;

        /// <summary>
        /// get and set AffectedCount
        /// </summary>
        public int AffectedCount = 0;

        /// <summary>
        /// get and set ProductionUnitWorkId used in diecasting create lot and label issue
        /// </summary>
        public int ProductionUnitWorkId { get; set; }
    }
}
