using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Mobile
{
    public interface IQRCodeGenerator
    {
        Stream CreateBarcode(string content);
    }
}
