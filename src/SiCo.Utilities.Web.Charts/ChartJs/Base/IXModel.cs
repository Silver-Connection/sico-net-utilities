namespace SiCo.Utilities.Web.Charts.ChartJs.Base
{
    using System.Collections.Generic;

    public interface IXModel
    {
        IList<decimal> Data { get; set; }

        string Label { get; set; }

        void AddValue(decimal value);

        void AddValue(decimal value, string color);

        void CloneValue();
    }
}