using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Entities;

namespace WpfApp1.Model
{
    public class ImageManager
    {

        public ImageManager() { 
        }

        public string ShowWindow(EntityType entity, ImageType image, int worldId, int? entityId = null)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            dlg.Title = "Select Icon";

            if (dlg.ShowDialog() == true)
            {
                string selectedFile = dlg.FileName;

                try
                {
                    string path = "";
                    if(entity == EntityType.World)
                    {
                        path = Create(
                            entity,
                            image,
                            worldId,
                            selectedFile);

                    }
                    else
                    {
                        path = Create(
                            entity,
                            image,
                            worldId,
                            selectedFile,
                            entityId);
                    }

                    return path;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving icon: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return "";
        }

        public string Create(EntityType entity, ImageType image, int worldId, string selectedFile, int? entityId = null)
        {
            string path = getFilePath(entity, image, worldId, entityId, null);
            Delete(entity, image, worldId, entityId, null);
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedFile);

            switch (image)
            {
                case ImageType.Icon:
                    bitmap.DecodePixelWidth = 64;
                    bitmap.DecodePixelHeight = 64;
                    break;
                case ImageType.Image:
                    bitmap.DecodePixelWidth = 300;
                    bitmap.DecodePixelHeight = 300;
                    break;
                case ImageType.Map:
                    bitmap.DecodePixelWidth = 500;
                    bitmap.DecodePixelHeight = 500;
                    break;
                case ImageType.Flag:
                    bitmap.DecodePixelWidth = 128;
                    bitmap.DecodePixelHeight = 128;
                    break;
            }
            bitmap.EndInit();

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
            }

            return path;

        }
        public string Delete(EntityType entity, ImageType image, int worldId, int? entityId = null, int? imageId = null)
        {
            string solutionRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\");
            string folderPath = Path.Combine(solutionRoot, "Res", "default.png");
            if (image == ImageType.Image && imageId == null) 
                return folderPath;

            string path = getFilePath(entity, image, worldId, entityId, imageId);

            var existing = File.Exists(path);
            if (existing)
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při mazání souboru {path}: {ex.Message}");
                }
            }

            return folderPath;
        }

        public void DeleteEntity(EntityType entity, int worldId, int? entityId = null)
        {
            string solutionRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"); // přibližně do root solution
            string folderPath = "";
            if (entity == EntityType.World)
            {
                folderPath = Path.Combine(solutionRoot, "Res", worldId.ToString());
            }
            else
            {
                folderPath = Path.Combine(solutionRoot, "Res", worldId.ToString(), $"{entity}-{entityId}");
            }

            if (Directory.Exists(folderPath))
            {
                try
                {
                    Directory.Delete(folderPath, recursive: true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při mazání složky {folderPath}: {ex.Message}");
                }
            }
        }
        public string GetDefaultImage()
        {
            string solutionRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"); // přibližně do root solution
            string folderPath = Path.Combine(solutionRoot, "Res", "default.png");
            return folderPath;
        }

        public BitmapImage LoadBitmap(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.StreamSource = stream;
            bmp.EndInit();
            bmp.Freeze();

            return bmp;
        }

        private string getFilePath(EntityType entity, ImageType image, int worldId, int? entityId, int? imageId)
        {
            string solutionRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\");
            string folderPath = "";
            if(entity == EntityType.World)
            {
                folderPath = Path.Combine(solutionRoot, "Res", worldId.ToString());
            }
            else
            {
                folderPath = Path.Combine(solutionRoot, "Res", worldId.ToString(), $"{entity}-{entityId}");
            }

            folderPath = Path.GetFullPath(folderPath);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string targetFile = "";

            switch (image)
            {
                case ImageType.Icon:
                    targetFile = Path.Combine(folderPath, "icon.png");
                    break;
                case ImageType.Image:
                    int index = -1;
                    if(imageId == null)
                    {
                        var files = Directory.GetFiles(folderPath);
                        Regex reg = new Regex(@"image-(\d+)\.png");


                        foreach (var file in files)
                        {
                            var fileName = Path.GetFileName(file);
                            var match = reg.Match(fileName);
                            if (match.Success && int.TryParse(match.Groups[1].Value, out int number))
                            {
                                if (number > index)
                                    index = number;
                            }
                        }
                        index++;
                    }
                    else
                    {
                        index = (int)imageId;
                    }

                    targetFile = Path.Combine(folderPath, $"image-{index}.png");
                    break;
                case ImageType.Map:
                    targetFile = Path.Combine(folderPath, "map.png");
                    break;
                case ImageType.Flag:
                    targetFile = Path.Combine(folderPath, "flag.png");
                    break;
            }

            return targetFile;
        }

        public int? IndexFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            var match = Regex.Match(path, @"image-(\d+)\.png$", RegexOptions.IgnoreCase);

            if (!match.Success)
                return null;

            return int.Parse(match.Groups[1].Value);
        }
    }
}
