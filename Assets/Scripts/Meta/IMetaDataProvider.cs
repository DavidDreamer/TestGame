public interface IMetaDataProvider
{
    MetaData Load();
    
    void Save(MetaData metaData);
}
