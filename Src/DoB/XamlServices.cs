// XamlServices

using DoB.Components;
using DoB.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Markup;

namespace DoB
{
    public class XamlServices : PrototypesLoaderExtension//MarkupExtension
    {
        public static object ObjectX;

        public XamlServices() 
        {
            Debug.WriteLine("[i] XamlServices");
            base.ProvideValue();
        }

        public object Parse(string xaml)
        {
            //TODO
            return default;
        }


        // Parse1 (manual mode)

        public Config Parse1(string xaml)
        {
            Config cfg = new Config();

            var doc = new XmlDocument();
            doc.LoadXml(xaml);


            IXmlNode xmlContent = default;
            object Result = default;

            XmlNodeList tags = doc.GetElementsByTagName("x:String");

            if (tags.Count > 0)
            {
                xmlContent = tags[0];//.First();
                Result = xmlContent.InnerText;
                //bad1 - firstContent.Attributes.GetNamedItem("StageDataFile").InnerText;
                //bad2 - xmlContent.InnerText;

                cfg["StageDataFile"] = (object)Result;
            }


            tags = doc.GetElementsByTagName("x:Int32");

            if (tags.Count > 0)
            {
                xmlContent = tags[0];//tags.First();
                Result = xmlContent.InnerText;

                cfg["ResolutionW"] = (object)Result;

                xmlContent = tags[1];
                Result = xmlContent.InnerText;

                cfg["ResolutionH"] = (object)Result;

            }

            tags = doc.GetElementsByTagName("x:Boolean");

            if (tags.Count > 0)
            {
                xmlContent = tags[0];//.First();
                Result = xmlContent.InnerText;

                cfg["IsFullScreen"] = (object)Result;
            }


            return cfg;
        }

        // Parse2 (manual mode)

        public DoB.Xaml.Stages Parse2(string xaml)
        {
            DoB.Xaml.Stages Stages = new DoB.Xaml.Stages();

            List<IComponent> ComponentsList = new List<IComponent>();

            // segment 1
            EnemySpawner E1_1 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy1",
                X = (double)1380,
                Y = (double)50
            };

            EnemySpawner E2_1 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy2",
                X = (double)1380,
                Y = (double)360
            };

            EnemySpawner E3_1 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy3",
                X = (double)1380,
                Y = (double)670
            };

            EnemySpawner E4_1 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 4,
                DelayMs = 26000,
                ReferenceName = "Stage1RC-ABEnemy1",
                X = (double)1380,
                Y = (double)-100
            };

            EnemySpawner E5_1 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 4,
                DelayMs = 26000,
                ReferenceName = "Stage1RC-ABEnemy2",
                X = (double)1380,
                Y = (double)820
            };

            EnemySpawner E6_1 = new EnemySpawner()
            {
                Count = 1,
                DelayMs = 54000,
                ReferenceName = "Stage1RC-TopBoss",
                X = (double)1380,
                Y = (double)50
            };

            EnemySpawner E7_1 = new EnemySpawner()
            {
                Count = 1,
                DelayMs = 54000,
                ReferenceName = "Stage1RC-BottomBoss",
                X = (double)1380,
                Y = (double)670
            };


            ComponentsList.Add(new Segment()
            {
                WaitForEvent = "eventCombination_0",
                Components = [E1_1, E2_1, E3_1, E4_1, E5_1, E6_1, E7_1],
                DelayMs = 0
            });

            // ++++++++++++++++++++++

            // segment 1
            EnemySpawner E1_2 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy1B",
                X = (double)1380,
                Y = (double)50
            };

            EnemySpawner E2_2 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy2B",
                X = (double)1380,
                Y = (double)360
            };

            EnemySpawner E3_2 = new EnemySpawner()
            {
                CooldownMs = (double)6000,
                Count = 4,
                ReferenceName = "Stage1RC-BEnemy3B",
                X = (double)1380,
                Y = (double)670
            };

            EnemySpawner E4_2 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 4,
                ReferenceName = "Stage1RC-ABEnemy1",
                DelayMs = 26000,
                X = (double)1380,
                Y = (double)-100
            };

            EnemySpawner E5_2 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 4,
                ReferenceName = "Stage1RC-ABEnemy2",
                DelayMs = 26000,
                X = (double)1380,
                Y = (double)820
            };

            EnemySpawner E6_2 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 2,
                ReferenceName = "Stage1RC-ABEnemy3",
                DelayMs = 26000,
                X = (double)1380,
                Y = (double)-100
            };

            EnemySpawner E7_2 = new EnemySpawner()
            {
                CooldownMs = (double)7000,
                Count = 2,
                ReferenceName = "Stage1RC-ABEnemy4",
                DelayMs = 26000,
                X = (double)1380,
                Y = (double)820
            };

            EnemySpawner E8_2 = new EnemySpawner()
            {
                Count = 1,
                ReferenceName = "Stage1RC-RingsBoss",
                DelayMs = 54000,
                X = (double)1100,
                Y = (double)360
            };

            EnemySpawner E9_2 = new EnemySpawner()
            {
                Count = 1,
                ReferenceName = "Stage1RC-FroggerBoss",
                WaitForEvent = "Stage1RC-RingsBossDied",
                X = (double)1100,
                Y = (double)360
            };

            EnemySpawner E10_2 = new EnemySpawner()
            {
                Count = 1,
                ReferenceName = "Stage2ZEnemy4",
                WaitForEvent = "Stage1RC-FroggerBossDied",
                X = (double)1400,
                Y = (double)360
            };


            ComponentsList.Add(new Segment()
            {
                WaitForEvent = "Stage1RC-TopBossDied;Stage1RC-BottomBossDied",
                Components = [E1_2, E2_2, E3_2, E4_2, E5_2, E6_2, E7_2, E8_2, E9_2, E10_2],
                DelayMs = 0
            });
            // ++++++++++++++++++++++
            // level 1
            Stage Stage1 = new Stage()
            {
                IsEnded = false,
                BackgroundTexture = default,
                BackgroundTextures = "Background_sil;Background_sil_buildings;Background_sil_foreground",
                BackgroundTextureArray = ["Background_sil", "Background_sil_buildings", "Background_sil_foreground"],
                EndsOnEvent = "Stage2BBossDied",
                Components = ComponentsList//[Components.Segment, Components.Segment]
            };


            Stages.Add(Stage1);


            // level 2
            Stage Stage2 = new Stage()
            {
                IsEnded = false,
                BackgroundTexture = default,
                BackgroundTextures = "Background_sil2;Background_sil_foreground2",
                BackgroundTextureArray = ["Background_sil2", "Background_sil_foreground2"],
                EndsOnEvent = "Stage2ZEnemy4Died",
                //Components = [DoB.Components.Segment, DoB.Components.Segment, DoB.Components.Segment]
            };

            Stages.Add(Stage2);

            // level 3
            Stage Stage3 = new Stage()
            {
                IsEnded = false,
                BackgroundTexture = "Background_castle",
                BackgroundTextures = default,
                BackgroundTextureArray = default,
                EndsOnEvent = "MiddleBossDied",
                //Components = [DoB.Components.EnemySpawner, DoB.Components.EnemySpawner, DoB.Components.EnemySpawner, DoB.Components.EnemySpawner...]
            };

            Stages.Add(Stage3);

            // level 4
            Stage Stage4 = new Stage()
            {
                IsEnded = false,
                BackgroundTexture = "sky",
                BackgroundTextures = default,
                BackgroundTextureArray = default,
                EndsOnEvent = "",
                //Components = []
            };

            Stages.Add(Stage4);

            return Stages;
        }

        // Parse3 (manual mode)
        public object Parse3(string xaml)
        {
            string stringsArray = "Prototypes-Common;Prototypes-1;Prototypes-2;PrototypesRC-1";

            ObjectX = base.ProvideValue(stringsArray);

            return ObjectX;
        }
    }
}