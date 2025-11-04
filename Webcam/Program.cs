using OpenCvSharp; // OpenCV library for image/video processing
using System;      // Basic .NET utilities (Console, etc.)

class Program
{
    static void Main(string[] args)
    {
        // Initialize webcam (0 = default camera)
        using var capture = new VideoCapture(0);
        using var window = new Window("Live Webcam Feed");

        if (!capture.IsOpened())
        {
            Console.WriteLine("Error: Unable to access the webcam.");
            return;
        }

        var frame = new Mat();
        bool filterOn = false; // Tracks if grayscale filter is enabled

        Console.WriteLine("Controls:");
        Console.WriteLine("'f' = toggle grayscale filter ON/OFF");
        Console.WriteLine("'c' = capture and save current image");
        Console.WriteLine("'q' = quit program");

        while (true)
        {
            // Read the current frame from the webcam
            capture.Read(frame);
            if (frame.Empty())
                break;

            // Make a copy for display (so we don’t overwrite the original frame)
            var displayFrame = frame.Clone();

            // Apply grayscale filter if toggled on
            if (filterOn)
            {
                Cv2.CvtColor(displayFrame, displayFrame, ColorConversionCodes.BGR2GRAY);
            }

            // Show either color or filtered video in the same window
            window.ShowImage(displayFrame);

            // Wait briefly for key press
            int key = Cv2.WaitKey(1);

            if (key == 'q') break; // Quit
            if (key == 'f')        // Toggle filter on/off
            {
                filterOn = !filterOn;
                Console.WriteLine(filterOn ? "Filter ON" : "Filter OFF");
            }
            if (key == 'c')        // Capture and save current image
            {
                Cv2.ImWrite("captured.jpg", frame);
                Console.WriteLine("Image captured and saved as 'captured.jpg'.");
            }
        }
    }
}