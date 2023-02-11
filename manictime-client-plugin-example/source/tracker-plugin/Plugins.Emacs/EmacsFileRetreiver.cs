using Finkit.ManicTime.Shared.DocumentTracking;
using ManicTime.Client.Tracker.EventTracking.Publishers.ApplicationTracking;

namespace Plugins.Emacs
{
    public class EmacsFileRetreiver : IDocumentRetreiver
    {
        public DocumentInfo GetDocument(ApplicationInfo application)
        {
            // first we use processName to see if it is Emacs. If it is not Emacs, we just return null and tracker will move on to the next plugin

            if (!CheckForProcess(application))
                return null;

            // get the filename
            string filename = GetFilename(application);

            // DocumentName - this will be displayed in the details view (bottom left part of ManicTime)
            // DocumentGroupName = this will be displayed on the bottom right side (group name)
            // DocumentType = file, url...


            // Emacs will only display filename, not the full path, so we will use the same filename for DocumentName and DocumentGroupName
            // if we could get the full path, for example c:\dir\filename.txt, then we could use 
            // 
            // DocumentName = c:\dir\filename.txt
            // DocumentGroupName = filename.txt

            if (filename != null)
                return new DocumentInfo() { DocumentName = filename, DocumentGroupName = filename, DocumentType = DocumentTypes.File };

            return null;
        }

        private bool CheckForProcess(ApplicationInfo application)
        {
            if (application.ProcessName == "emacs")
                return true;

            return false;
        }

        private string GetFilename(ApplicationInfo application)
        {
            return application.WindowTitle.Trim();
        }
    }
}
