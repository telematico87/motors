using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Shared.Commons
{
    public static class eCommerceConstants
    {
        public const string MAIN_IMAGES_DIRECTORY = "/content/images/";
        public const string THUMBNAIL_FOLDERNAME = "thumbnails/";
        public const string IMAGES_THUMBNAIL_DIRECTORY = "/content/images/" + THUMBNAIL_FOLDERNAME;
        public const string LOADING_PICTURE = "site/loading.gif";
        public const string DEFAULT_PICTURE = "site/default-picture.png";
        public const string USER_DEFAULT_PICTURE = "site/user-default-avatar.png";
        public const int CATALOGO_MOTO_ID = 1;
        public const int CATALOGO_ACCESORIO_ID = 2;
        public const int CATALOGO_LLANTA_ID = 2;
        public const int CATALOGO_LUBRICANTE_ID = 3;
        public const int TALLA_S = 1;
        public const int TALLA_M = 2;
        public const int TALLA_L = 3;
        public const int TALLA_XL = 4;
        public const int TALLA_XXL = 5;
    }
}
