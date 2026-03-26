namespace LogService.Infractructure.ElasticSearch
{
    internal record ElasticSearchOptions(
        string Host,
        string UserName,
        string Password
    );
}
