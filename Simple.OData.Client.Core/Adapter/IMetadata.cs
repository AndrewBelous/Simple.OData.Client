﻿using System.Collections.Generic;

#pragma warning disable 1591

namespace Simple.OData.Client
{
    public interface IMetadata
    {
        ISession Session { get; }

        EntityCollection GetEntityCollection(string collectionPath);
        EntityCollection GetDerivedEntityCollection(EntityCollection baseCollection, string entityTypeName);

        string GetEntityCollectionExactName(string collectionName);
        string GetEntityCollectionTypeName(string collectionName);
        string GetEntityCollectionTypeNamespace(string collectionName);
        string GetEntityCollectionQualifiedTypeName(string collectionName);
        bool EntityCollectionRequiresOptimisticConcurrencyCheck(string collectionName);

        string GetEntityTypeExactName(string collectionName);

        bool IsOpenType(string collectionName);
        IEnumerable<string> GetStructuralPropertyNames(string collectionName);
        bool HasStructuralProperty(string collectionName, string propertyName);
        string GetStructuralPropertyExactName(string collectionName, string propertyName);
        IEnumerable<string> GetDeclaredKeyPropertyNames(string collectionName);

        bool HasNavigationProperty(string collectionName, string propertyName);
        string GetNavigationPropertyExactName(string collectionName, string propertyName);
        string GetNavigationPropertyPartnerName(string collectionName, string propertyName);
        bool IsNavigationPropertyCollection(string collectionName, string propertyName);

        string GetFunctionFullName(string functionName);
        EntityCollection GetFunctionReturnCollection(string functionName);
        string GetActionFullName(string actionName);
        EntityCollection GetActionReturnCollection(string functionName);

        EntryDetails ParseEntryDetails(string collectionName, IDictionary<string, object> entryData, string contentId = null);
    }
}