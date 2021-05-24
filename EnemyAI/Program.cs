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
            var trainer = mlContext.Regression.Trainers.LightGbm(new LightGbmRegressionTrainer.Options() { NumberOfIterations = 200, LearningRate = 0.2030963f, NumberOfLeaves = 7, MinimumExampleCountPerLeaf = 1, UseCategoricalSplit = true, HandleMissingValue = false, UseZeroAsMissingValue = true, MinimumExampleCountPerGroup = 100, MaximumCategoricalSplitPointCount = 8, CategoricalSmoothing = 10, L2CategoricalRegularization = 10, Booster = new GradientBooster.Options() { L2Regularization = 1, L1Regularization = 1 }, LabelColumnName = "col13", FeatureColumnName = "Features" });

            IEstimator<ITransformer> trainingPipeline = dataProcessPipeline.Append(trainer);

            // Traino il modello con i dati passati nel trainingDataView
            ITransformer mlModel = trainingPipeline.Fit(trainingDataView);

            //Salva il modello sotto forma di zip
            mlContext.Model.Save(mlModel, trainingDataView.Schema, (Directory.GetCurrentDirectory() + "\\.\\.\\Data\\Modello.zip"));
            string str = Directory.GetCurrentDirectory() + "\\.\\.\\Data\\Modello.zip";
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());

            //Creo un nuovo modello
            CreateModel();


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

            //Testo il modello 
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
            Console.WriteLine($"\n\nAzione Nemica: {risultato.Score}\n\n");
        }
    }
}
