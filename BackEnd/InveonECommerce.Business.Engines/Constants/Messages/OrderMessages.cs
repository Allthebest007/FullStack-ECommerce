using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Constants.Messages
{
    public class OrderMessages
    {
        public static string FailAdded = " Order ekleme işlemi sırasında hata meydana geldi.";
        public static string SuccessfullyAdded = " Order başarıyla eklendi.";
        public static string FailUpdated = " Order güncelleme sırasında hata meydana geldi";
        public static string NotFound = " Order bulunamadı";
        public static string FailDeleted = " Order silme işlemi sırasında hata oldu";
    }
}
