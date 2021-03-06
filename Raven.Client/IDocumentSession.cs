﻿using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Raven.Client.Client;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace Raven.Client
{
    public interface IDocumentSession : IDisposable
    {
        T Load<T>(string id);

        T[] Load<T>(params string[] ids);

        void Refresh<T>(T entity);

        void Commit(Guid txId);

        void Rollback(Guid txId);

        IRavenQueryable<T> Query<T>(string indexName);

		IDocumentQuery<T> LuceneQuery<T>(string indexName);
        
		void SaveChanges();

        string StoreIdentifier { get; }
        
        void Store<T>(T entity);
        
        void Delete<T>(T entity);

        void Evict<T>(T entity);
        
        void Clear();
        
        bool UseOptimisticConcurrency { get; set; }

    	DocumentConvention Conventions { get; }

        int MaxNumberOfRequestsPerSession { get; set; }

        event EntityStored Stored;

        event EntityToDocument OnEntityConverted;

        JObject GetMetadataFor<T>(T instance);
    }

    public delegate void EntityStored(object entity);

    public delegate void EntityToDocument(object entity, JObject document, JObject metadata);
}
