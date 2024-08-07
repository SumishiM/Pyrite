﻿namespace Pyrite.Assets
{
    public abstract class GameAsset
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
        public string? Extention { get; set; }

        public Guid Guid { get; internal set; }

        public GameAsset()
        {
            Guid = new();
        }

        public GameAsset(string path)
        {
            path  = path.Replace("\\", "/");
            Name = System.IO.Path.GetFileNameWithoutExtension(path.Split('/').Last());
            Extention = System.IO.Path.GetExtension(path.Split('/').Last());
            Path = path.Replace(Name + Extention, string.Empty);
            Guid = AssetDatabase.CreateGuidFromAssetPath(path);
        }
    }
}
