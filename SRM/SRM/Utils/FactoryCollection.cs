using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using SRM.Models;

namespace SRM.Utils
{
    public class FactoryCollection
    {
        public FactoryCollection()
        {

        }

        public SRMEntities Database => Singleton.GetDatabase();

        public string CreatePhotoById(int id, string owner)
        {
            var path = string.Empty;
            var tbPhoto = Database.tbPhotoes.FirstOrDefault(x => x.id == id);
            var photo = tbPhoto?.Photo;
            if (photo == null) return path;
            path = Path.Combine(Path.GetTempPath(), $@"{owner}.jpg");
            var imgPath = Image.FromStream(new MemoryStream(photo));
            imgPath.Save(path);
            return path;
        }

        public string CreateDefaultPhoto()
        {
            var path = string.Empty;
            var photo = Database.tbPhotoes.FirstOrDefault(x => x.isdefault == true).Photo;
            if (photo == null) return path;
            path = Path.Combine(Path.GetTempPath(), $@"SRMPhoto_{DateTime.Now.ToString("yyyyMMdd")}.jpg");
            var imgPath = Image.FromStream(new MemoryStream(photo));
            imgPath.Save(path);
            return path;
        }
    }
}