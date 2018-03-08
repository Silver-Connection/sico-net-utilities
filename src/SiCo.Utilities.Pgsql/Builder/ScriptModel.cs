namespace SiCo.Utilities.Pgsql.Builder
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Cerate SQL scripts
    /// </summary>
    public class ScriptModel
    {
        /// <summary>
        /// Spacer #1
        /// </summary>
        public static string SpacerA = "	";

        /// <summary>
        /// Spacer #2
        /// </summary>
        public static string SpacerB = "		";

        private IList<string> lines = new List<string>();

        /// <summary>
        /// Init
        /// </summary>
        public ScriptModel()
        {
            this.Comment = string.Empty;
            this.Content = new List<string>();
            this.Declare = new List<DeclareModel>();
            this.Name = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="name">Script Name</param>
        /// <param name="comment">Script Comment</param>
        public ScriptModel(string name, string comment)
            : this()
        {
            this.Comment = comment;
            this.Name = name;
        }

        /// <summary>
        /// Script Comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Script Content
        /// </summary>
        public IList<string> Content { get; set; }

        /// <summary>
        /// Declare Model
        /// </summary>
        public IList<DeclareModel> Declare { get; set; }

        /// <summary>
        /// Script Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Add Insert Model to Script Model
        /// </summary>
        /// <param name="model">Insert Model</param>
        /// <param name="suffix">suffix to use for declared variable name</param>
        /// <param name="injects">Inject values</param>
        /// <param name="skipComment">Include comments</param>
        /// <returns>Declared variable name if used</returns>
        public string AddInsert(InsertModel model, string suffix, IEnumerable<KeyValuePair<string, string>> injects = null, bool skipComment = false)
        {
            if (!skipComment)
            {
                this.Content.Add($"-- {model.FullName}");
                this.Content.Add("--------------------------------------------------");
            }

            var declare = string.Empty;
            if (model.HasId)
            {
                declare = ($"i_{model.Table}_id_{suffix}").Trim('_');
                this.Declare.Add(new DeclareModel(declare));
                this.Content.Add($"{model.Build(true, injects).TrimEnd(';')} INTO {declare};");
                this.Content.Add($"RAISE NOTICE '{model.FullName} ID: %', {declare};");
            }
            else
            {
                this.Content.Add($"{model.Build(true, injects)}");
            }

            this.Content.Add(string.Empty);
            return declare;
        }

        /// <summary>
        /// Build script file
        /// </summary>
        /// <returns></returns>
        public virtual string[] Build()
        {
            this.lines.Clear();

            this.lines.Add("--------------------------------------------------");
            this.lines.Add($"-- Script      : {this.Name}");
            this.lines.Add($"-- Comment     : {this.Comment}");
            this.lines.Add("--------------------------------------------------");
            this.lines.Add(string.Empty);

            this.lines.Add("DO");
            this.lines.Add("$$");
            this.lines.Add("DECLARE");
            this.lines.Add(string.Empty);

            // Add declares
            foreach (var item in this.Declare)
            {
                this.lines.Add($"{SpacerA}{item.Name} {item.Type.ToUpper()};");
            }

            this.lines.Add("BEGIN");

            // Add content
            foreach (var item in this.Content)
            {
                this.lines.Add($"{SpacerA}{item}");
            }

            this.lines.Add("END;");
            this.lines.Add("$$;");

            return this.lines.ToArray();
        }
    }
}