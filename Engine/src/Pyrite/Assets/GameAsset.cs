

//namespace Pyrite.Assets
//{
//    internal abstract class GameAsset
//    {
//        public string Name { get; set; } = string.Empty;
//        public string Description { get; set; } = string.Empty;

//        [JsonProperty]
//        public Guid Guid { get; private set; }

//        public virtual bool CanBeCreated => true;
//        public virtual bool CanBeRenamed => true;
//        public virtual bool CanBeDeleted => true;
//        public virtual bool CanBeSaved => true;

//        /** **/

//        private string _filePath = string.Empty;

//        /// <summary>
//        /// Path to this asset file, relative to its base directory where this asset is stored.
//        /// </summary>
//        [JsonIgnore]
//        public string FilePath
//        {
//            get => _filePath;
//            set
//            {
//                _filePath = value.EscapePath();

//                _nameSplit = null;
//                _simplifiedName = null;
//            }
//        }


//        private bool _fileChanged = false;

//        [JsonIgnore]
//        public bool FileChanged
//        {
//            get => _fileChanged;
//            set
//            {
//                _fileChanged = value;
//                OnModified();
//            }
//        }

//        private bool _rename = false;

//        /// <summary>
//        /// Whether it should rename the file and delete the previous name.
//        /// </summary>
//        [JsonIgnore]
//        public bool Rename
//        {
//            get => _rename;
//            set
//            {
//                _rename = value;
//                FileChanged = value;
//            }
//        }

//        [JsonIgnore]
//        public bool TaggedForDeletion = false;

//        public virtual void AfterDeserialized() { }

//        public void MakeGuid()
//        {
//            Guid = Guid.NewGuid();
//        }

//        /// <summary>
//        /// Create a duplicate of the current asset.
//        /// </summary>
//        public GameAsset Duplicate(string name)
//        {
//            GameAsset asset = SerializationHelper.DeepCopy(this);

//            asset.Name = name;
//            asset.MakeGuid();

//            return asset;
//        }

//        /// <summary>
//        /// Implemented by assets that may cache data.
//        /// This notifies it that it has been modified (usually by an editor).
//        /// </summary>
//        protected virtual void OnModified() { }
//    }
//}
