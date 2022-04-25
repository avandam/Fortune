using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortune.Logic.Exceptions;
using Fortune.Logic.Fields;

namespace Fortune.Logic
{
    /// <summary>
    ///  Creates all the static data for the game. Game will create the fields using this data.
    /// </summary>
    public class GameData
    {
        // TODO: Maybe Create Dictionaries between enumerationvalues and content instead of the linq queries?
        //       But: is it worth the extra overhead of the indexer?
        private static List<Zone> zones;
        private static List<Resource> resources;
        private static List<Certificate> certificates;
        private static List<ResourceType> resourceTypes;
        private static Random random = new Random();

        public GameData()
        {
        }

        public static void InitializeData()
        {
            InitializeRandomResources();
            CreateZones();
            CreateResources();
            CreateCertificates();
        }

        private static void InitializeRandomResources()
        {
            resourceTypes = new List<ResourceType>
            {
                ResourceType.NaturalGas,
                ResourceType.Aluminum,
                ResourceType.CarIndustry,
                ResourceType.Cocoa,
                ResourceType.Gold,
                ResourceType.Harbors,
                ResourceType.Iron,
                ResourceType.Coffee,
                ResourceType.Copper,
                ResourceType.Lead,
                ResourceType.Nickel,
                ResourceType.Oil,
                ResourceType.Rice,
                ResourceType.Rubber,
                ResourceType.Cotton,
                ResourceType.Shipyards,
                ResourceType.Steel,
                ResourceType.Coal,
                ResourceType.Sugar,
                ResourceType.Wheat,
                ResourceType.Tea,
                ResourceType.Uranium,
                ResourceType.Wool,
                ResourceType.Silver,
                ResourceType.NaturalGas,
                ResourceType.Aluminum,
                ResourceType.CarIndustry,
                ResourceType.Cocoa,
                ResourceType.Gold,
                ResourceType.Harbors,
                ResourceType.Iron,
                ResourceType.Coffee,
                ResourceType.Copper,
                ResourceType.Lead,
                ResourceType.Nickel,
                ResourceType.Oil,
                ResourceType.Rice,
                ResourceType.Rubber,
                ResourceType.Cotton,
                ResourceType.Shipyards,
                ResourceType.Steel,
                ResourceType.Coal,
                ResourceType.Sugar,
                ResourceType.Wheat,
                ResourceType.Tea,
                ResourceType.Uranium,
                ResourceType.Wool,
                ResourceType.Silver,
            };
        }

        public static Resource GetRandomResource()
        {
            int nextResource = random.Next(0, resourceTypes.Count);
            Resource resource = GetResource(resourceTypes[nextResource]);
            resourceTypes.RemoveAt(nextResource);
            return resource;
        }
    
        private static void CreateZones()
        {
            zones = new List<Zone>();
            zones.Add(new Zone(CountryType.Benelux, ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone(CountryType.WestGermany, ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone(CountryType.GreatBritain, ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone(CountryType.France, ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone(CountryType.SouthEurope, ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone(CountryType.EastEurope, ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone(CountryType.Balkan, ContinentType.Europe, Color.LimeGreen));
            zones.Add(new Zone(CountryType.USSR, ContinentType.None, Color.DarkGreen));
            zones.Add(new Zone(CountryType.Canada, ContinentType.America, Color.Red));
            zones.Add(new Zone(CountryType.Mexico, ContinentType.America, Color.Red));
            zones.Add(new Zone(CountryType.Caribbean, ContinentType.America, Color.Red));
            zones.Add(new Zone(CountryType.Venezuela, ContinentType.America, Color.Red));
            zones.Add(new Zone(CountryType.Andes, ContinentType.America, Color.Red));
            zones.Add(new Zone(CountryType.Brazil, ContinentType.America, Color.Red));
            zones.Add(new Zone(CountryType.Argentina, ContinentType.America, Color.Red));
            zones.Add(new Zone(CountryType.USA, ContinentType.None, Color.DarkOrange));
            zones.Add(new Zone(CountryType.Morocco, ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone(CountryType.Ethiopia, ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone(CountryType.WestAfrica, ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone(CountryType.CentralAfrica, ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone(CountryType.EastAfrica, ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone(CountryType.SouthAfrica, ContinentType.Africa, Color.SaddleBrown));
            zones.Add(new Zone(CountryType.Oceania, ContinentType.Oceania, Color.Purple));
            zones.Add(new Zone(CountryType.MiddleEast, ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone(CountryType.India, ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone(CountryType.SouthEastAsia, ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone(CountryType.Japan, ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone(CountryType.China, ContinentType.Asia, Color.Yellow));
            zones.Add(new Zone(CountryType.Indonesia, ContinentType.Asia, Color.Yellow));
        }

        public static Zone GetZone(CountryType name)
        {
            return zones.First(zone => zone.Country == name);
        }

        private static void CreateResources()
        {
            resources = new List<Resource>(); 
            resources.Add(new Resource(ResourceType.NaturalGas, 500000));
            resources.Add(new Resource(ResourceType.Aluminum, 900000));
            resources.Add(new Resource(ResourceType.CarIndustry, 800000));
            resources.Add(new Resource(ResourceType.Cocoa, 400000));
            resources.Add(new Resource(ResourceType.Gold, 1000000));
            resources.Add(new Resource(ResourceType.Harbors, 600000));
            resources.Add(new Resource(ResourceType.Iron, 1000000));
            resources.Add(new Resource(ResourceType.Coffee, 400000));
            resources.Add(new Resource(ResourceType.Copper, 900000));
            resources.Add(new Resource(ResourceType.Lead,600000));
            resources.Add(new Resource(ResourceType.Nickel, 800000));
            resources.Add(new Resource(ResourceType.Oil, 1200000));
            resources.Add(new Resource(ResourceType.Rice, 700000));
            resources.Add(new Resource(ResourceType.Rubber, 400000));
            resources.Add(new Resource(ResourceType.Cotton, 500000));
            resources.Add(new Resource(ResourceType.Shipyards, 700000));
            resources.Add(new Resource(ResourceType.Steel, 1100000));
            resources.Add(new Resource(ResourceType.Coal, 1100000));
            resources.Add(new Resource(ResourceType.Sugar, 900000));
            resources.Add(new Resource(ResourceType.Wheat, 1000000));
            resources.Add(new Resource(ResourceType.Tea, 500000));
            resources.Add(new Resource(ResourceType.Uranium, 800000));
            resources.Add(new Resource(ResourceType.Wool, 500000));
            resources.Add(new Resource(ResourceType.Silver, 700000));
        }

        public static Resource GetResource(ResourceType name)
        {
            return resources.First(resource => resource.Name == name);
        }

        private static void CreateCertificates()
        {
            Zone zone;
            certificates = new List<Certificate>();

            zone = GetZone(CountryType.Benelux);
            certificates.Add(new Certificate(GetResource(ResourceType.Steel), 10, 500000, zone, RegionType.Netherlands));
            certificates.Add(new Certificate(GetResource(ResourceType.NaturalGas), 10, 2000000, zone, RegionType.Netherlands));
            certificates.Add(new Certificate(GetResource(ResourceType.Shipyards), 5, 1500000, zone, RegionType.Netherlands));
            certificates.Add(new Certificate(GetResource(ResourceType.Harbors), 30, 2500000, zone, RegionType.Netherlands));
            certificates.Add(new Certificate(GetResource(ResourceType.CarIndustry), 10, 1000000, zone, RegionType.Netherlands));
            certificates.Add(new Certificate(GetResource(ResourceType.CarIndustry), 5, 500000, zone, RegionType.Belgium));
            certificates.Add(new Certificate(GetResource(ResourceType.Harbors), 10, 1500000, zone, RegionType.Belgium));
            certificates.Add(new Certificate(GetResource(ResourceType.Iron), 5, 500000, zone, RegionType.Belgium));
            certificates.Add(new Certificate(GetResource(ResourceType.Coal), 5, 500000, zone, RegionType.Belgium));

            zone = GetZone(CountryType.WestGermany);
            certificates.Add(new Certificate(GetResource(ResourceType.Steel), 10, 1000000, zone, RegionType.None));
            certificates.Add(new Certificate(GetResource(ResourceType.CarIndustry), 15, 1500000, zone, RegionType.None));
            certificates.Add(new Certificate(GetResource(ResourceType.Shipyards), 15, 1000000, zone, RegionType.None));



        }

        public static List<Certificate> GetCertificatesForCountry(CountryType country)
        {
            return certificates.Where(certificate => certificate.Zone.Country == country).ToList();
        }

        public static Certificate GetCertificate(Resource resource, Zone zone, RegionType region)
        {
            return certificates.First(certificate => certificate.Resource == resource && certificate.Zone == zone && certificate.Region == region);
        }
    }
}
