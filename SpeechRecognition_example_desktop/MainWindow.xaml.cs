using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using NAudio.Wave;


namespace SpeechRecognition_example
{
    public partial class MainWindow : Window
    {
        private WaveInEvent waveIn;
        private WaveFileWriter waveWriter;
        private string outputFilePath = "recording_to_recognition.wav";
        private bool isRecording = false;

        public MainWindow()
        {
            InitializeComponent();
            waveIn = new WaveInEvent();
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;

        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter != null)
            {
                waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
                waveWriter.Flush();
            }
        }

        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveWriter != null)
            {
                waveWriter.Close();
                waveWriter = null;
            }
            if (e.Exception != null)
            {
                main_label.Content = "Błąd nagrywania: " + e.Exception.Message;
            }
            else
            {
                main_label.Content = "Nagrywanie zakończone.";
            }
        }

        private void Button_start_recording_Click(object sender, RoutedEventArgs e)
        {
            if (!isRecording)
            {
                waveWriter = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                waveIn.StartRecording();
                isRecording = true;
                main_label.Content = "Nagrywanie...";
            }
            else
            {
                waveIn.StopRecording();
                isRecording = false;
            }
        }

        private void Button_display_recognized_text_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(outputFilePath))
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var content = new MultipartFormDataContent();
                        var fileStream = new FileStream(outputFilePath, FileMode.Open);
                        content.Add(new StreamContent(fileStream), "file", "recording_to_recognition.wav");

                        var response = client.PostAsync("http://localhost:8000/recognize-speech", content).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = response.Content.ReadAsStringAsync().Result;
                            main_label.Content = "Odpowiedź od API: " + responseContent;
                        }
                        else
                        {
                            main_label.Content = "Błąd zapytania API: " + response.StatusCode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    main_label.Content = "Błąd: " + ex.Message;
                }
            }
            else
            {
                main_label.Content = "Plik nagrania nie istnieje.";
            }
        }

        private void Button_clear_main_label_Click(object sender, RoutedEventArgs e)
        {
            main_label.Content = "Tu wyświetli się rozpoznany tekst...";
        }
    }
}