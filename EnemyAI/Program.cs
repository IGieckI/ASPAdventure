using Microsoft.ML;
using Microsoft.ML.Trainers.LightGbm;
using System;
using System.IO;

namespace EnemyAI
{
    class Program
    {
        // Creo l'MLContext per fare tutte le cose AI
        private static MLContext mlContext = new MLContext();
        
        //Creo quello che sarà il corpo dell'ia
        private static Lazy<PredictionEngine<Input, Output>> PredictionEngine = new Lazy<PredictionEngine<Input, Output>>(CreatePredictionEngine);

        public static PredictionEngine<Input, Output> CreatePredictionEngine()
        {
            // Carico il modello precedentemente creato
            ITransformer mlModel = mlContext.Model.Load((Directory.GetCurrentDirectory() + "\\Data\\Modello.zip"), out var modelInputSchema);

            //Creo un prediction engine in cui poter inserire dei dati per testare se è stata tutta una perdita di tempo o qualche neurone a posto c'è l'ha
            return mlContext.Model.CreatePredictionEngine<Input, Output>(mlModel);
        }

        public static void CreateModel()
        {
            Console.WriteLine("Creazione modello...");
            // Indico il file da cui prendere i fati con tutte le specifiche sull'input
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<Input>(
                                            path: (Directory.GetCurrentDirectory()+"\\Data\\Data.txt"),
                                            hasHeader: false,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            //Creo una pipeline che mi permette di "convertire" i dati dal formato che gli passo io a qualcosa che l'IA possa comprendere quindi
            //una pipeline che concatena diverse colonne di input(definite nella classe input) in una nuova colonna di output
            var dataProcessPipeline = mlContext.Transforms.Concatenate("Features", new[] { "col0", "col1", "col2", "col3", "col4", "col5", "col6", "col7", "col8", "col9", "col10", "col11", "col12" });

            // Definisco l'algoritmo che andrò ad utilizzare
            var trainer = mlContext.Regression.Trainers.LightGbm(new LightGbmRegressionTrainer.Options() { /*NumberOfIterations = 200, LearningRate = 0.2030963f, NumberOfLeaves = 7, MinimumExampleCountPerLeaf = 1, UseCategoricalSplit = true, HandleMissingValue = false, UseZeroAsMissingValue = true, MinimumExampleCountPerGroup = 100, MaximumCategoricalSplitPointCount = 8, CategoricalSmoothing = 10, L2CategoricalRegularization = 10, Booster = new GradientBooster.Options() { L2Regularization = 1, L1Regularization = 1 },*/ Booster = new DartBooster.Options(){TreeDropFraction = 0.15,XgboostDartMode = false}, LabelColumnName = "col13", FeatureColumnName = "Features" });

            IEstimator<ITransformer> trainingPipeline = dataProcessPipeline.Append(trainer);

            Console.WriteLine("Training del modello...");
            // Traino il modello con i dati passati nel trainingDataView
            ITransformer mlModel = trainingPipeline.Fit(trainingDataView);

            Console.WriteLine("Esportazione del modello...");
            //Salva il modello sotto forma di zip
            mlContext.Model.Save(mlModel, trainingDataView.Schema, (Directory.GetCurrentDirectory() + "\\Data\\Modello.zip"));
            string str = Directory.GetCurrentDirectory() + "\\Data\\Modello.zip";
        }

        public static void TestModel()
        {
            int nTest=0;
            int nSuccess = 0;
            using(StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\Data\\Data.txt"))
            {
                while(!sr.EndOfStream)
                {
                    string[] input = sr.ReadLine().ToString().Split(',');
                    Input dati = new Input()
                    {
                        Col0 = float.Parse(input[0]),
                        Col1 = float.Parse(input[1]),
                        Col2 = float.Parse(input[2]),
                        Col3 = float.Parse(input[3]),
                        Col4 = float.Parse(input[4]),
                        Col5 = float.Parse(input[5]),
                        Col6 = float.Parse(input[6]),
                        Col7 = float.Parse(input[7]),
                        Col8 = float.Parse(input[8]),
                        Col9 = float.Parse(input[9]),
                        Col10 = float.Parse(input[10]),
                        Col11 = float.Parse(input[11]),
                        Col12 = float.Parse(input[12])
                    };

                    var result = PredictionEngine.Value.Predict(dati);
                    nTest++;
                    if (Math.Round(result.Score,0) == float.Parse(input[13]))
                        nSuccess++;
                    Console.WriteLine($"Percentuale di accuratezza: {100*nSuccess/nTest}%");
                }                
            }
        }

        static void Main(string[] args)
        {

            //Creo un nuovo modello
            CreateModel();

            //Testo il modello 
            TestModel();
            
            Console.WriteLine("\nTest dati:");
            Input dati = new Input()
            {
                Col0 = 26F,
                Col1 = 11F,
                Col2 = 21F,
                Col3 = 7F,
                Col4 = 8F,
                Col5 = 20F,
                Col6 = 14F,
                Col7 = 9F,
                Col8 = 8F,
                Col9 = 1F,
                Col10 = 0F,
                Col11 = 6F,
                Col12 = 3F,
            };
            var risultato = PredictionEngine.Value.Predict(dati);

            Console.WriteLine($"PlayerHp: {dati.Col0}");
            Console.WriteLine($"PlayerMana: {dati.Col1}");
            Console.WriteLine($"PlayerAttack: {dati.Col2}");
            Console.WriteLine($"PlayerIntelligence: {dati.Col3}");
            Console.WriteLine($"PlayerMagicCost: {dati.Col4}");
            Console.WriteLine($"EnemyHp: {dati.Col5}");
            Console.WriteLine($"EnemyMana: {dati.Col6}");
            Console.WriteLine($"EnemyAttack: {dati.Col7}");
            Console.WriteLine($"EnemyIntelligence: {dati.Col8}");
            Console.WriteLine($"HasManaPotion: {dati.Col9}");
            Console.WriteLine($"HasHpPotion: {dati.Col10}");
            Console.WriteLine($"EnemyMagicHealCost: {dati.Col11}");
            Console.WriteLine($"EnemyMagicDamageCost: {dati.Col12}");
            Console.Write($"\n\nAzione Nemica: {risultato.Score} ");

            switch(Math.Round(risultato.Score))
            {
                case 0:
                    Console.Write("(Attacco Normale)");
                    break;
                case 1:
                    Console.Write("(Attacco Magico)");
                    break;
                case 2:
                    Console.Write("(Cura)");
                    break;
                case 3:
                    Console.Write("(Pozione HP)");
                    break;
                case 4:
                    Console.Write("(Pozione Mana)");
                    break;
            }
            Console.ReadKey();
        }
    }
}
