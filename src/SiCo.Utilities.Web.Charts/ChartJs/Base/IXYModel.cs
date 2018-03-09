namespace SiCo.Utilities.Web.Charts.ChartJs.Base
{
    using System.Collections.Generic;

    public interface IXYModel
    {
        IList<DataPointXYModel<string, decimal>> Data { get; set; }

        string Label { get; set; }

        void AddValue(DataPointXYModel<string, decimal> value);

        void AddValue(DataPointXYModel<string, decimal> value, string color);

        void AddValue(string x, decimal y);

        void AddValue(string x, decimal y, string color);
    }
}