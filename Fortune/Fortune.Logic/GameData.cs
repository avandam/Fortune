using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortune.Logic.Fields;

namespace Fortune.Logic
{
    public class GameData
    {
        // TODO: Maybe Create Dictionaries between enumerationvalues and content?
        private static List<Zone> zones = new List<Zone>();
        private static List<Resource> resources = new List<Resource>();
        private static List<Certificate> certificates = new List<Certificate>();

        public List<Field> Fields { get; }
        

        public GameData()
        {
            Fields = new List<Field>();
        }

        public void CreateFields(Game game)
        {
        }

        public static void CreateZones()
        {
            zones = new List<Zone>();
            zones.Add(new Zone("Benelux", ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone("West-Duitsland", ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone("Groot-Brittanie", ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone("Frankrijk", ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone("Zuid-Europa", ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone("Oost-Europa", ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone("Balkan", ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone("U.S.S.R.", ContinentType.None, Color.DarkGreen));
            zones.Add(new Zone("Canada", ContinentType.America, Color.Red));
            zones.Add(new Zone("Mexico", ContinentType.America, Color.Red));
            zones.Add(new Zone("Caribisch Gebied", ContinentType.America, Color.Red));
            zones.Add(new Zone("Venezuela", ContinentType.America, Color.Red));
            zones.Add(new Zone("Andes", ContinentType.America, Color.Red));
            zones.Add(new Zone("Brazilie", ContinentType.America, Color.Red));
            zones.Add(new Zone("Argentinie", ContinentType.America, Color.Red));
            zones.Add(new Zone("Verenigde Staten", ContinentType.None, Color.DarkOrange));
            zones.Add(new Zone("Marokko", ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone("Ethiopie", ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone("West-Afrika", ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone("Centraal Afrika", ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone("Oost-Afrika", ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone("Zuid-Afrika", ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone("Oceanie", ContinentType.Oceania, Color.Purple));
            zones.Add(new Zone("Midden Oosten", ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone("Voor Indie", ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone("Zuid-Oost Azie", ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone("Japan", ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone("China", ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone("Indonesie", ContinentType.Asia, Color.Yellow));
        }

        public static Zone GetZone(string name)
        {
            return zones.First(zone => zone.Country == name);
        }

        public static void CreateResources()
        {
            resources = new List<Resource>(); 
            resources.Add(new Resource("Aardgas", 500000));
            resources.Add(new Resource("Aluminium", 900000));
            resources.Add(new Resource("Auto-Industrie", 800000));
            resources.Add(new Resource("Cacao", 400000));
            resources.Add(new Resource("Goud", 1000000));
            resources.Add(new Resource("Havens", 600000));
            resources.Add(new Resource("IJzer", 1000000));
            resources.Add(new Resource("Koffie", 400000));
            resources.Add(new Resource("Koper", 900000));
            resources.Add(new Resource("Lood", 600000));
            resources.Add(new Resource("Nikkel", 800000));
            resources.Add(new Resource("Olie", 1200000));
            resources.Add(new Resource("Rijst", 700000));
            resources.Add(new Resource("Rubber", 400000));
            resources.Add(new Resource("Ruwe Katoen", 500000));
            resources.Add(new Resource("Scheepsbouw", 700000));
            resources.Add(new Resource("Staal", 1100000));
            resources.Add(new Resource("Steenkool", 1100000));
            resources.Add(new Resource("Suiker", 900000));
            resources.Add(new Resource("Tarwe", 1000000));
            resources.Add(new Resource("Thee", 500000));
            resources.Add(new Resource("Uranium", 800000));
            resources.Add(new Resource("Wol", 500000));
            resources.Add(new Resource("Zilver", 700000));
        }

        public static Resource GetResource(string name)
        {
            return resources.First(resource => resource.Name == name);
        }

        public static void CreateCertificates()
        {
            Resource resource;
            certificates = new List<Certificate>();

            resource = GetResource("Aardgas");
            certificates.Add(new Certificate(resource, 35, 500000, GetZone("Verenigde Staten"), string.Empty));
            certificates.Add(new Certificate(resource, 25, 500000, GetZone("U.S.S.R."), string.Empty));
            certificates.Add(new Certificate(resource, 10, 2000000, GetZone("Benelux"), "Nederland"));
            certificates.Add(new Certificate(resource, 10, 1000000, GetZone("Canada"), string.Empty));
            certificates.Add(new Certificate(resource, 5, 500000, GetZone("China"), string.Empty));
            certificates.Add(new Certificate(resource, 5, 1000000, GetZone("Groot-Brittanie"), string.Empty));
        }
    }
}
