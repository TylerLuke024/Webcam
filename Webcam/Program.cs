using OpenCvSharp; // Import the OpenCV library for image/video processing
using System;      // Import basic .NET functionality like Console I/O

class Program
{
    static void Main(string[] args)
    {
        // Initialize the webcam (0 = default camera)
        using var capture = new VideoCapture(0);

        // Create a window to display the webcam feed
        using var window = new Window("Live Webcam Feed");

        // Check if the webcam is accessible
        if (!capture.IsOpened())
        {
            Console.WriteLine("Error: Unable to access the webcam.");
            return; // Exit the program if webcam is unavailable
        }

        // Create a Mat (matrix) object to store each video frame
        var frame = new Mat();
        Console.WriteLine("Press 'c' to capture an image or 'q' to quit.");

        // Main loop for live video streaming
        while (true)
        {
            // Read the current frame from the webcam
            capture.Read(frame);

            // If no frame is captured, break the loop
            if (frame.Empty())
                break;

            // Display the current frame in the OpenCV window
            window.ShowImage(frame);

            // Wait briefly for a key press (1 millisecond)
            int key = Cv2.WaitKey(1);

            // Quit if the user presses 'q'
            if (key == 'q') break;

            // Capture image if the user presses 'c'
            if (key == 'c')
            {
                // Save the current frame as an image
                Cv2.ImWrite("captured.jpg", frame);
                Console.WriteLine("Image captured!");

                // Apply a grayscale filter to the captured image
                ApplyFilter(frame);
            }
        }
    }

    // Method that applies a grayscale filter to the captured image
    static void ApplyFilter(Mat original)
    {
        // Create a new Mat to store the grayscale image
        var gray = new Mat();

        // Convert the color image (BGR) to grayscale
        Cv2.CvtColor(original, gray, ColorConversionCodes.BGR2GRAY);

        // Display the grayscale image in a new window
        Cv2.ImShow("Filtered (Grayscale) Image", gray);

        // Save the grayscale image to disk
        Cv2.ImWrite("filtered.jpg", gray);

        Console.WriteLine("Filter applied and image saved as 'filtered.jpg'.");

        // Wait for a key press before closing the window
        Cv2.WaitKey(0);
    }
}