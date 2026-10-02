namespace SchoolFormApp;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        // This switch renders the real interface for the README screenshot.
        if (args.Length == 2 && args[0] == "--screenshot")
        {
            using var preview = new MainForm(showDemoData: true);
            preview.RenderScreenshot(args[1]);
            return;
        }

        Application.Run(new MainForm());
    }
}
