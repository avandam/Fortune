using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
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
        private static List<Field> fields;
        private static List<ResourceType> resourceTypes;
        private static List<TelexCard> telexCards;
        private static readonly Random random = new Random();

        private GameData()
        {
        }

        public static void InitializeData(Game game)
        {
            InitializeRandomResources();
            CreateZones();
            CreateResources();
            CreateCertificates();
            CreateTelexCards();
            CreateFields(game);
        }

        private static void CreateTelexCards()
        {
            telexCards = new List<TelexCard>();
            telexCards.Add(new TelexCard("Gefeliciteerd! - stop - u bent jarig en elke speler geeft u {0} miljoen.", TelexType.GainFromPlayers, 1000000)); 
            telexCards.Add(new TelexCard("Groei in de wereldeconomie - stop - Staalindustrieën zijn winstgevend - stop - u ontvangt {0} als u {1} bezit; zoniet dan ontvangt u {2}.", TelexType.Gain, 9000000, 7000000, new List<Resource> {GetResource(ResourceType.Steel)}));
            telexCards.Add(new TelexCard("Internationale spanning - stop - prijzen van {1} springen omhoog - stop - u ontvangt {0} als u {1} bezit; zo niet dan ontvangt u {2}.", TelexType.Gain, 6000000, 4000000, new List<Resource> {GetResource(ResourceType.Uranium)}));
            telexCards.Add(new TelexCard("Uitstekende {1}oogst - stop - u ontvangt {0} als u {1} bezit; zo niet dan ontvangt u {2}.", TelexType.Gain, 4000000, 2000000, new List<Resource> {GetResource(ResourceType.Rubber)}));
            telexCards.Add(new TelexCard("Prachtige {1} oogst - stop - u ontvangt {0} als u {1} bezit; zo niet dan ontvangt u {2}.", TelexType.Gain, 7000000, 5000000, new List<Resource> {GetResource(ResourceType.Wheat)}));
            telexCards.Add(new TelexCard("U heeft vergeten uw verzekering te verlengen - stop - dieven hebben uw huis leeggehaald - stop - er is voor {0} aan geld en juwelen gestolen. Betaal dit bedrag aan de bank.", TelexType.Loss, 3000000));
            telexCards.Add(new TelexCard("U verkoopt uw terreinen in Zuid-Afrika waar een uraniumveld is ontdekt - stop - u ontvangt {0}.", TelexType.Gain, 5000000));
            telexCards.Add(new TelexCard("Uw installaties om {1}erts te delven beantwoorden niet aan de gestelde veiligheidseisen - stop - er is een ongeluk gebeurd - stop - betaal de boete van {0} aan de bank als u {1} heeft.", TelexType.Loss, 7000000, new List<Resource> {GetResource(ResourceType.Iron) }));
            telexCards.Add(new TelexCard("Groot succes in de autoraces - stop - uw merk rijdt altijd aan kop - stop - u ontvangt {0} als u {1} bezit; zo niet dan ontvangt u {2}", TelexType.Gain, 6000000, 4000000, new List<Resource> {GetResource(ResourceType.CarIndustry) }));
            telexCards.Add(new TelexCard("uw laatste boek over wereldhandel is bekroond met een internationale prijs - stop - het publiek vecht erom - stop de auteursrechten leveren u {0} op.", TelexType.Gain, 4000000));
            telexCards.Add(new TelexCard("Een aardbeving vernietigt de scheepsbouwwerven van Yokohama en Osaka - stop - u betaalt {0} als u {1} bezit; zo niet dan betaalt u {2}.", TelexType.Loss, 5000000, 3000000, new List<Resource> {GetResource(ResourceType.Shipyards) }));
            telexCards.Add(new TelexCard("Toevloeiing van kapitaal - stop - de goudmarkt zakt in - stop - u betaalt {0} als u {1} bezit; zo niet dan betaalt u {2}.", TelexType.Loss, 7000000, 5000000, new List<Resource> {GetResource(ResourceType.Gold)}));
            telexCards.Add(new TelexCard("Uw oom in Amerika vindt olie op zijn grond in Texas - stop - hij geeft u een kadootje - stop - u ontvangt {0}.", TelexType.Gain, 6000000));
            telexCards.Add(new TelexCard("Uw onderzoeksvloot ontdekt een antiek galeischip met een grote schat aan boord - stop - de verkoop van die schat brengt {0} op.", TelexType.Gain, 3000000));
            telexCards.Add(new TelexCard("Een hevige cycloon richt grote schade aan in het zuiden van de Verenigde Staten - stop de katoen en tarwevelden zijn verwoest - stop - u betaalt {0} als u {1} bezit; zo niet dan betaalt u {2}.", TelexType.Loss, 5000000, 3000000, new List<Resource> { GetResource(ResourceType.Cotton), GetResource(ResourceType.Wheat)}));
            telexCards.Add(new TelexCard("Incident in Zaire - stop - de koperreserves van Katanga blijken inexploitabel te zijn - stop - u betaalt {0} als u {1} bezit; zo niet dan betaalt u {2}.", TelexType.Loss, 6000000, 4000000, new List<Resource> { GetResource(ResourceType.Copper)}));
            telexCards.Add(new TelexCard("Overproductie van koffie - stop - u betaalt {0} als u {1} bezit; zo niet dan betaalt u {2}.", TelexType.Loss, 4000000, 2000000, new List<Resource> {GetResource(ResourceType.Coffee) }));
            telexCards.Add(new TelexCard("Een van de uw fabrieken is door brand verwoest - stop - u was slecht verzekerd - stop - u moet {0} betalen voor de herbouw.", TelexType.Loss, 1000000));
        }

        public static TelexCard GetTelexCard()
        {
            return telexCards[random.Next(0, telexCards.Count - 1)];
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
            int nextResource = random.Next(0, resourceTypes.Count - 1);
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
            certificates.Add(new Certificate(GetResource(ResourceType.Steel), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.CarIndustry), 15, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Shipyards), 15, 1000000, zone));

            zone = GetZone(CountryType.France);
            certificates.Add(new Certificate(GetResource(ResourceType.Uranium), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Wheat), 10,500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Harbors), 10, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.CarIndustry), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Shipyards),10, 500000, zone));

            zone = GetZone(CountryType.SouthEurope);
            certificates.Add(new Certificate(GetResource(ResourceType.Steel), 5, 500000, zone, RegionType.Italy));
            certificates.Add(new Certificate(GetResource(ResourceType.Shipyards), 5, 500000, zone, RegionType.Italy));

            zone = GetZone(CountryType.GreatBritain);
            certificates.Add(new Certificate(GetResource(ResourceType.Coal), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.NaturalGas), 5, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Harbors), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Shipyards), 15, 1000000, zone));

            zone = GetZone(CountryType.EastEurope);
            certificates.Add(new Certificate(GetResource(ResourceType.Coal), 15, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Aluminum), 5, 500000, zone, RegionType.Hungary));

            zone = GetZone(CountryType.Balkan);
            certificates.Add(new Certificate(GetResource(ResourceType.Nickel), 5, 500000, zone, RegionType.Greece));
            certificates.Add(new Certificate(GetResource(ResourceType.Cotton), 5, 500000, zone, RegionType.Turkey));

            zone = GetZone(CountryType.USSR);
            certificates.Add(new Certificate(GetResource(ResourceType.Gold), 30, 2500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Wheat), 30, 3000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Oil), 30, 3000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Iron), 30, 3500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Nickel), 25, 2500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Steel), 25, 3000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Cotton), 25, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.NaturalGas), 25, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Copper), 20, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Wool), 20, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Coal), 20, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Sugar), 20, 2500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Lead), 20, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Tea), 5, 500000, zone));

            zone = GetZone(CountryType.Canada);
            certificates.Add(new Certificate(GetResource(ResourceType.Nickel), 25, 2500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Uranium), 20, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Lead), 15, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Silver), 15, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Copper), 15, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Gold), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.NaturalGas), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Wheat), 10, 500000, zone));
            
            zone = GetZone(CountryType.Mexico);
            certificates.Add(new Certificate(GetResource(ResourceType.Silver), 20, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Coffee), 15, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Lead), 10, 1000000, zone));

            zone = GetZone(CountryType.Caribbean);
            certificates.Add(new Certificate(GetResource(ResourceType.Aluminum), 20, 2000000, zone, RegionType.Jamaica));
            certificates.Add(new Certificate(GetResource(ResourceType.Sugar), 15, 1500000, zone, RegionType.Cuba));
            certificates.Add(new Certificate(GetResource(ResourceType.Nickel), 10, 1000000, zone, RegionType.Cuba));

            zone = GetZone(CountryType.Venezuela);
            certificates.Add(new Certificate(GetResource(ResourceType.Aluminum), 10, 1000000, zone, RegionType.Suriname));
            certificates.Add(new Certificate(GetResource(ResourceType.Aluminum), 5, 500000, zone, RegionType.Guyana));
            certificates.Add(new Certificate(GetResource(ResourceType.Oil), 5, 500000, zone));

            zone = GetZone(CountryType.Andes);
            certificates.Add(new Certificate(GetResource(ResourceType.Copper), 20, 2000000, zone, RegionType.Chili));
            certificates.Add(new Certificate(GetResource(ResourceType.Coffee), 15, 500000, zone, RegionType.Colombia));
            certificates.Add(new Certificate(GetResource(ResourceType.Silver), 15, 1500000, zone, RegionType.Peru));
            certificates.Add(new Certificate(GetResource(ResourceType.Lead), 10, 500000, zone, RegionType.Peru));
            certificates.Add(new Certificate(GetResource(ResourceType.Cocoa), 5, 500000, zone, RegionType.Ecuador));

            zone = GetZone(CountryType.Brazil);
            certificates.Add(new Certificate(GetResource(ResourceType.Coffee), 35, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Cocoa), 20, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Sugar), 20, 2500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Iron), 15, 1500000, zone));

            zone = GetZone(CountryType.Argentina);
            certificates.Add(new Certificate(GetResource(ResourceType.Wool), 10, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Wool), 5, 500000, zone, RegionType.Uruguay));

            zone = GetZone(CountryType.USA);
            certificates.Add(new Certificate(GetResource(ResourceType.Uranium), 40, 3000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.NaturalGas), 35, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.CarIndustry), 30, 3500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Cotton), 25, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Oil), 25, 2500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Copper), 25, 2500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Coal), 25, 3000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Steel), 25, 3000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Lead), 20, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Harbors), 15, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Iron), 15, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Silver), 15, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Wheat), 15, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Sugar), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Gold), 5, 500000, zone));

            zone = GetZone(CountryType.Morocco);
            certificates.Add(new Certificate(GetResource(ResourceType.Uranium), 5, 500000, zone));

            zone = GetZone(CountryType.Ethiopia);
            certificates.Add(new Certificate(GetResource(ResourceType.Coffee), 10, 500000, zone));

            zone = GetZone(CountryType.WestAfrica);
            certificates.Add(new Certificate(GetResource(ResourceType.Aluminum), 20, 2000000, zone, RegionType.Guinea));
            certificates.Add(new Certificate(GetResource(ResourceType.Cocoa), 20, 1000000, zone, RegionType.IvoryCoast));
            certificates.Add(new Certificate(GetResource(ResourceType.Cocoa), 20, 1000000, zone, RegionType.Ghana));
            certificates.Add(new Certificate(GetResource(ResourceType.Cocoa), 15, 1000000, zone, RegionType.Nigeria));
            certificates.Add(new Certificate(GetResource(ResourceType.Cocoa), 10, 500000, zone, RegionType.Cameroon));
            certificates.Add(new Certificate(GetResource(ResourceType.Coffee), 10, 500000, zone, RegionType.IvoryCoast));
            certificates.Add(new Certificate(GetResource(ResourceType.Rubber), 5, 500000, zone, RegionType.Nigeria));

            zone = GetZone(CountryType.CentralAfrica);
            certificates.Add(new Certificate(GetResource(ResourceType.Copper), 5, 500000, zone, RegionType.Zaire));
            certificates.Add(new Certificate(GetResource(ResourceType.Uranium), 5, 500000, zone, RegionType.Gabon));

            zone = GetZone(CountryType.EastAfrica);
            certificates.Add(new Certificate(GetResource(ResourceType.Copper), 10, 1000000, zone, RegionType.Zambia));
            certificates.Add(new Certificate(GetResource(ResourceType.Tea), 5, 500000, zone, RegionType.Kenya));

            zone = GetZone(CountryType.SouthAfrica);
            certificates.Add(new Certificate(GetResource(ResourceType.Gold), 40, 4500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Uranium), 15, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Nickel), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Wool), 5, 500000, zone));

            zone = GetZone(CountryType.Oceania);
            certificates.Add(new Certificate(GetResource(ResourceType.Aluminum), 35, 3500000, zone, RegionType.Australia));
            certificates.Add(new Certificate(GetResource(ResourceType.Wool), 35, 2000000, zone, RegionType.Australia));
            certificates.Add(new Certificate(GetResource(ResourceType.Iron), 20, 2000000, zone, RegionType.Australia));
            certificates.Add(new Certificate(GetResource(ResourceType.Silver), 15, 1500000, zone, RegionType.Australia));
            certificates.Add(new Certificate(GetResource(ResourceType.Lead), 15, 500000, zone, RegionType.Australia));
            certificates.Add(new Certificate(GetResource(ResourceType.Nickel), 15, 1500000, zone, RegionType.NewCaledonia));
            certificates.Add(new Certificate(GetResource(ResourceType.Wool), 15, 1000000, zone, RegionType.NewZealand));
            certificates.Add(new Certificate(GetResource(ResourceType.Gold), 5, 500000, zone, RegionType.NewGuinea));

            zone = GetZone(CountryType.MiddleEast);
            certificates.Add(new Certificate(GetResource(ResourceType.Oil), 20, 2000000, zone, RegionType.SaudiArabia));
            certificates.Add(new Certificate(GetResource(ResourceType.Oil), 10, 1000000, zone, RegionType.Iran));
            certificates.Add(new Certificate(GetResource(ResourceType.Oil), 5, 500000, zone, RegionType.Iraq));

            zone = GetZone(CountryType.India);
            certificates.Add(new Certificate(GetResource(ResourceType.Tea), 35, 2500000, zone, RegionType.India));
            certificates.Add(new Certificate(GetResource(ResourceType.Rice), 20, 2000000, zone, RegionType.India));
            certificates.Add(new Certificate(GetResource(ResourceType.Wheat), 15, 1000000, zone, RegionType.India));
            certificates.Add(new Certificate(GetResource(ResourceType.Cotton), 15, 1000000, zone, RegionType.India));
            certificates.Add(new Certificate(GetResource(ResourceType.Sugar), 15, 1500000, zone, RegionType.India));
            certificates.Add(new Certificate(GetResource(ResourceType.Tea), 15, 1000000, zone, RegionType.SriLanka));
            certificates.Add(new Certificate(GetResource(ResourceType.Cotton), 10, 500000, zone, RegionType.Pakistan));
            certificates.Add(new Certificate(GetResource(ResourceType.Rubber), 10, 500000, zone, RegionType.India));
            certificates.Add(new Certificate(GetResource(ResourceType.Rubber), 5, 500000, zone, RegionType.SriLanka));

            zone = GetZone(CountryType.SouthEastAsia);
            certificates.Add(new Certificate(GetResource(ResourceType.Rubber), 35, 1500000, zone, RegionType.Malaysia));
            certificates.Add(new Certificate(GetResource(ResourceType.Rubber), 15, 500000, zone, RegionType.Thailand));
            certificates.Add(new Certificate(GetResource(ResourceType.Rice), 10, 1000000, zone, RegionType.Myanmar));
            certificates.Add(new Certificate(GetResource(ResourceType.Rice), 10, 1000000, zone, RegionType.Thailand));

            zone = GetZone(CountryType.Japan);
            certificates.Add(new Certificate(GetResource(ResourceType.Shipyards), 40, 3000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.CarIndustry), 20, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Harbors), 15, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Steel), 15, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Tea), 10, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Rice), 10, 500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Silver), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Gold), 5, 500000, zone));

            zone = GetZone(CountryType.China);
            certificates.Add(new Certificate(GetResource(ResourceType.Rice), 35, 2500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Tea), 25, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Cotton), 20, 1500000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Coal), 20, 2000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Wheat), 15, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Sugar), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Iron), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.NaturalGas), 5, 1000000, zone));

            zone = GetZone(CountryType.Indonesia);
            certificates.Add(new Certificate(GetResource(ResourceType.Rubber), 25, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Rice), 10, 1000000, zone));
            certificates.Add(new Certificate(GetResource(ResourceType.Coffee), 10, 500000, zone));
        }

        public static List<Certificate> GetCertificatesForCountry(CountryType country)
        {
            return certificates.Where(certificate => certificate.Zone.Country == country).ToList();
        }

        public static Certificate GetCertificate(Resource resource, Zone zone, RegionType region)
        {
            return certificates.First(certificate => certificate.Resource == resource && certificate.Zone == zone && certificate.Region == region);
        }

        private static void CreateFields(Game game)
        {
            fields = new List<Field>();
            fields.Add(new Area(game, 0, GetRandomResource(), GetCertificatesForCountry(CountryType.Benelux)));
            fields.Add(new Area(game, 1, GetRandomResource(), GetCertificatesForCountry(CountryType.WestGermany)));
            fields.Add(new ChoiceContinent(game, 2, GetRandomResource(), new List<ContinentType> {ContinentType.Europe}));
            fields.Add(new Area(game, 3, GetRandomResource(), GetCertificatesForCountry(CountryType.GreatBritain)));
            fields.Add(new Area(game, 4, GetRandomResource(), GetCertificatesForCountry(CountryType.France)));
            fields.Add(new Area(game, 5, GetRandomResource(), GetCertificatesForCountry(CountryType.SouthEurope)));
            fields.Add(new ChoiceContinent(game, 6, GetRandomResource(), new List<ContinentType> {ContinentType.Europe}));
            fields.Add(new Area(game, 7, GetRandomResource(), GetCertificatesForCountry(CountryType.EastEurope)));
            fields.Add(new Area(game, 8, GetRandomResource(), GetCertificatesForCountry(CountryType.Balkan)));
            fields.Add(new Auction(game, 9));
            fields.Add(new Bonus(game, 10, GetRandomResource()));
            fields.Add(new Telex(game, 11));
            fields.Add(new Area(game, 12, GetRandomResource(), GetCertificatesForCountry(CountryType.USSR)));
            fields.Add(new Area(game, 13, GetRandomResource(), GetCertificatesForCountry(CountryType.USSR)));
            fields.Add(new Area(game, 14, GetRandomResource(), GetCertificatesForCountry(CountryType.USSR)));
            fields.Add(new Telex(game, 15));
            fields.Add(new Bonus(game, 16, GetRandomResource()));
            fields.Add(new Joker(game, 17));
            fields.Add(new Area(game, 18, GetRandomResource(), GetCertificatesForCountry(CountryType.Canada)));
            fields.Add(new Area(game, 19, GetRandomResource(), GetCertificatesForCountry(CountryType.Mexico)));
            fields.Add(new ChoiceContinent(game, 20, GetRandomResource(), new List<ContinentType> {ContinentType.America}));
            fields.Add(new Area(game, 21, GetRandomResource(), GetCertificatesForCountry(CountryType.Caribbean)));
            fields.Add(new Area(game, 22, GetRandomResource(), GetCertificatesForCountry(CountryType.Venezuela)));
            fields.Add(new Area(game, 23, GetRandomResource(), GetCertificatesForCountry(CountryType.Andes)));
            fields.Add(new ChoiceContinent(game, 24, GetRandomResource(), new List<ContinentType> { ContinentType.America }));
            fields.Add(new Area(game, 25, GetRandomResource(), GetCertificatesForCountry(CountryType.Brazil)));
            fields.Add(new Area(game, 26, GetRandomResource(), GetCertificatesForCountry(CountryType.Argentina)));
            fields.Add(new Auction(game, 27));
            fields.Add(new Bonus(game, 28, GetRandomResource()));
            fields.Add(new Telex(game, 29));
            fields.Add(new Area(game, 30, GetRandomResource(), GetCertificatesForCountry(CountryType.USA)));
            fields.Add(new Area(game, 31, GetRandomResource(), GetCertificatesForCountry(CountryType.USA)));
            fields.Add(new Area(game, 32, GetRandomResource(), GetCertificatesForCountry(CountryType.USA)));
            fields.Add(new Telex(game, 33));
            fields.Add(new Bonus(game, 34, GetRandomResource()));
            fields.Add(new Auction(game, 35));
            fields.Add(new Area(game, 36, GetRandomResource(), GetCertificatesForCountry(CountryType.Morocco)));
            fields.Add(new Area(game, 37, GetRandomResource(), GetCertificatesForCountry(CountryType.Ethiopia)));
            fields.Add(new Area(game, 38, GetRandomResource(), GetCertificatesForCountry(CountryType.WestAfrica)));
            fields.Add(new ChoiceContinent(game, 39, GetRandomResource(), new List<ContinentType> { ContinentType.Africa }));
            fields.Add(new Area(game, 40, GetRandomResource(), GetCertificatesForCountry(CountryType.CentralAfrica)));
            fields.Add(new Area(game, 41, GetRandomResource(), GetCertificatesForCountry(CountryType.EastAfrica)));
            fields.Add(new Area(game, 42, GetRandomResource(), GetCertificatesForCountry(CountryType.SouthAfrica)));
            fields.Add(new Joker(game, 43));
            fields.Add(new Bonus(game, 44, GetRandomResource()));
            fields.Add(new ChoiceWorld(game, 45));
            fields.Add(new Area(game, 46, GetRandomResource(), GetCertificatesForCountry(CountryType.Oceania)));
            fields.Add(new Telex(game, 47));
            fields.Add(new Bonus(game, 48, GetRandomResource()));
            fields.Add(new Auction(game, 49));
            fields.Add(new Area(game, 50, GetRandomResource(), GetCertificatesForCountry(CountryType.MiddleEast)));
            fields.Add(new Area(game, 51, GetRandomResource(), GetCertificatesForCountry(CountryType.India)));
            fields.Add(new Area(game, 52, GetRandomResource(), GetCertificatesForCountry(CountryType.SouthEastAsia)));
            fields.Add(new ChoiceContinent(game, 53, GetRandomResource(), new List<ContinentType> { ContinentType.Asia, ContinentType.Oceania }));
            fields.Add(new Area(game, 54, GetRandomResource(), GetCertificatesForCountry(CountryType.Japan)));
            fields.Add(new Area(game, 55, GetRandomResource(), GetCertificatesForCountry(CountryType.China)));
            fields.Add(new Area(game, 56, GetRandomResource(), GetCertificatesForCountry(CountryType.Indonesia)));
            fields.Add(new Joker(game, 57));
            fields.Add(new Bonus(game, 58, GetRandomResource()));
            fields.Add(new ChoiceWorld(game, 59));
            fields.Add(new Area(game, 60, GetRandomResource(), GetCertificatesForCountry(CountryType.Oceania)));
            fields.Add(new Telex(game, 61));
            fields.Add(new Bonus(game, 62, GetRandomResource()));
            fields.Add(new Auction(game, 63));
        }

        public static Field GetField(int number)
        {
            return fields.First(field => field.Number == number);
        }

        public static void SetFields(List<Field> newFields)
        {
            fields = newFields;
        }

        public static int GetNumberOfFields()
        {
            return fields.Count;
        }

        public static List<Field> GetFieldsForContinents(List<ContinentType> continents)
        {
            return fields.Where(field => field is Area area && continents.Contains(area.Zone.Continent)).ToList();
        }

        public static List<Field> GetAreas()
        {
            return fields.Where(field => field is Area).ToList();
        }

        public static Field GetFieldByZone(Zone zone)
        {
            return fields.First(field => field.Zone == zone);
        }
    }
}
