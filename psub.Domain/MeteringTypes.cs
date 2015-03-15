using System.ComponentModel;

namespace Psub.Domain
{
    public enum MeteringType
    {
        [Description("Размер, мм")]
        L = 1,
        [Description("Дистанция, см")]
        D = 2,
        [Description("Температура, С")]
        T = 3,
        [Description("Напряжение, В")]
        U = 4,
        [Description("Ток, А")]
        I = 5
    }
}
