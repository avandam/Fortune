using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public enum CountryType
    {
        [Text("Benelux")] Benelux,
        [Text("West-Duitsland")] WestGermany,
        [Text("Groot-Britannië")] GreatBritain,
        [Text("Frankrijk")] France,
        [Text("Zuid-Europa")] SouthEurope,
        [Text("Oost-Europa")] EastEurope,
        [Text("Balkan")] Balkan,
        [Text("U.S.S.R.")] USSR,
        [Text("Canada")] Canada,
        [Text("Mexico")] Mexico,
        [Text("Caribisch Gebied")] Caribbean,
        [Text("Venezuela")] Venezuela,
        [Text("Andes")] Andes,
        [Text("Brazilië")] Brazil,
        [Text("Argentinië")] Argentina,
        [Text("Verenigde Staten")] USA,
        [Text("Marokko")] Morocco,
        [Text("Ethiopië")] Ethiopia,
        [Text("West-Afrika")] WestAfrica,
        [Text("Centraal Afrika")] CentralAfrica,
        [Text("Oost-Afrika")] EastAfrica,
        [Text("Zuid-Afrika")] SouthAfrica,
        [Text("Oceanië")] Oceania,
        [Text("Midden Oosten")] MiddleEast,
        [Text("Voor-Indië")] India,
        [Text("Zuid-Oost Azië")] SouthEastAsia,
        [Text("Japan")] Japan,
        [Text("China")] China,
        [Text("Indonesië")] Indonesia
    }

    public enum ContinentType
    {
        [Text("Europa")] Europe,
        [Text("Amerika")] America,
        [Text("Afrika")] Africa,
        [Text("Oceanië")] Oceania,
        [Text("Azië")] Asia,
        [Text("")] None
    }

    public enum ResourceType
    {
        [Text("Aardgas")] NaturalGas,
        [Text("Aluminium")] Aluminum,
        [Text("Auto-Industrie")] CarIndustry,
        [Text("Cacao")] Cocoa,
        [Text("Goud")] Gold,
        [Text("Havens")] Harbors,
        [Text("IJzer")] Iron,
        [Text("Koffie")] Coffee,
        [Text("Koper")] Copper,
        [Text("Lood")] Lead,
        [Text("Nikkel")] Nickel,
        [Text("Olie")] Oil,
        [Text("Rijst")] Rice,
        [Text("Rubber")] Rubber,
        [Text("Ruwe Katoen")] Cotton,
        [Text("Scheepsbouw")] Shipyards,
        [Text("Staal")] Steel,
        [Text("Steenkool")] Coal,
        [Text("Suiker")] Sugar,
        [Text("Tarwe")] Wheat,
        [Text("Thee")] Tea,
        [Text("Uranium")] Uranium,
        [Text("Wol")] Wool,
        [Text("Zilver")] Silver
    }

    public class TextAttribute : Attribute
    {
        public string Text;

        public TextAttribute(string text)
        {
            Text = text;
        }
    }

    public static class EnumExtender
    {
        public static string ToText(this Enum enumeration)
        {
            MemberInfo[] memberInfo = enumeration.GetType().GetMember(enumeration.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(TextAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    return ((TextAttribute)attributes[0]).Text;
                }
            }

            return enumeration.ToString();
        }
    }
}
