using System;
using System.IO;

namespace AppBackup
{
    /// <summary>
    /// Program Class
    /// </summary>
    class Backup
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main(string[] args)
        {
            double history;     // Days to keep backups for
            string source;      // Source directory
            string target;      // Target directory
            string filter = "";         // extentions to copy

            // Ensure we have the right amount of arguements
            if (args.Length < 2 || args.Length > 4)
            {
                Console.WriteLine("Usage: AppBackUp <sourcefolder> <targetfolder> <history> <filter> \n<sourcefolder>\t\tFolder to copy\n<targetfolder>\t\tFolder to copy into\n<history>\t\tDays of history to keep. Anything older gets deleted. 0 = forever\n[<filter>]\t\tOptional: Comma delimited string of file extenstions to include. (exp: .pdf,.docx,.doc,.xls). Defaults to all files.");
            }
            else
            {
                target = args[1]; // set target
                // build timestamp for target
                string temp = System.DateTime.Today.Year.ToString() + "-" + System.DateTime.Today.Month.ToString() + "-" + System.DateTime.Today.Day.ToString();
                if (!target.EndsWith(@"\"))
                {
                    target += @"\";
                }
                target += temp;   // append timestamp to target
                Console.WriteLine("target=" + target);    // echo the target
                source = args[0]; // get the source

                short result = 0;

                // if we have the optional [<filter>] param
                if (args.Length == 4)
                {
                    filter = args[3];
                }

                Backup.CopyDirectory(source, target, filter);    // pass off to subroutine to do the actual backup

                try
                {
                    // Attemp a parse on the param to int
                    if (System.Int16.TryParse(args[2], out result))
                    {
                        // Delete anything older than [history] days agao
                        history = Convert.ToDouble(args[2]);
                        if (history > 0)
                        {
                            System.DateTime d;
                            d = System.DateTime.Now.Subtract(System.TimeSpan.FromDays(history));
                            DirectoryInfo p = Directory.GetParent(target);
                            string[] dirs = Directory.GetDirectories(p.FullName);
                            for (int i = 0; i < dirs.Length; i++)
                            {
                                System.DateTime t = Directory.GetLastWriteTime(dirs[i]);
                                if (t < d)
                                    Directory.Delete(dirs[i], true);
                            }
                        }
                    }
                }
                catch (Exception) { Console.WriteLine("Error occurred deleting history."); }

            }
        }


        /// <summary>
        /// Copies the [source] to the [target]
        /// </summary>
        /// <param name="SourceDirectory"></param>
        /// <param name="TargetDirectory"></param>
        /// <param name="filter"></param>
        static private void CopyDirectory(string SourceDirectory, string TargetDirectory, string filter)
        {
            // get the directories
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            DirectoryInfo target = new DirectoryInfo(TargetDirectory);
            DirectoryInfo[] dirs = source.GetDirectories();
            // dump filter into collection
            System.Collections.Generic.List<string> ext = new System.Collections.Generic.List<string>(filter.Split(','));

            //Determine whether the source directory exists. create if !exists
            if (!source.Exists)
                return;
            if (!target.Exists)
                target.Create();

            //Copy files.
            FileInfo[] sourceFiles = source.GetFiles();

            // if we have a filter, apply
            if (filter != "")
            {
                for (int i = 0; i < sourceFiles.Length; ++i)
                    if (ext.Contains(sourceFiles[i].Extension))
                    {
                        copyfile(sourceFiles[i].FullName, target.FullName, sourceFiles[i].Name);
                    }

                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(TargetDirectory, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, filter);
                }
            }
            else  // if no filter, just copy everything
            {
                for (int i = 0; i < sourceFiles.Length; ++i)
                {
                    copyfile(sourceFiles[i].FullName, target.FullName, sourceFiles[i].Name);
                }


                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(TargetDirectory, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, "");
                }


            }

            // some cleanup
            source = null;
            target = null;
            ext = null;

        }

        /// <summary>
        /// Copies a single file
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        static private void copyfile(string source, string target, string name)
        {
            try
            {
                File.Copy(source, target + "\\" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Millisecond.ToString() + "-" + name, true);

            }
            catch (Exception)
            {
                Console.WriteLine("Error copying file: " + source);
            }
        }


    }
}
