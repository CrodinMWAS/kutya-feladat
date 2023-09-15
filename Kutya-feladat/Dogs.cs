using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutya_feladat
{
    internal class Dogs
    {
        public int id;
        public int typeId;
        public int nameId;
        public int age;
        public string lastMedicalCheck;

        public string typeName;
        public string ogName;

        public string dogName;

        public Dogs(int id, int typeId, int nameId, int age, string lastMedicalCheck, string typeName, string ogName, string dogName)
        {
            this.id = id;
            this.typeId = typeId;
            this.nameId = nameId;
            this.age = age;
            this.lastMedicalCheck = lastMedicalCheck;
            this.typeName = typeName;
            this.ogName = ogName;
            this.dogName = dogName;
        }
    }
}
