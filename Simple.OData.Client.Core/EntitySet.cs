using System;
using System.Collections.Generic;
using System.Linq;
using Simple.OData.Client.Extensions;

#pragma warning disable 1591

namespace Simple.OData.Client
{
    public class EntitySet
    {
        private readonly string _actualName;
        private readonly EntitySet _baseEntitySet;
        private readonly IMetadata _metadata;

        internal EntitySet(string name, EntitySet baseEntitySet, IMetadata metadata)
        {
            _actualName = name;
            _baseEntitySet = baseEntitySet;
            _metadata = metadata;
        }

        public override string ToString()
        {
            return _actualName;
        }

        public string ActualName
        {
            get { return _actualName; }
        }

        public EntitySet BaseEntitySet
        {
            get { return _baseEntitySet; }
        }

        public IMetadata Metadata
        {
            get { return _metadata; }
        }

        public EntitySet FindDerivedEntitySet(string entityTypeName)
        {
            var actualName = _metadata.GetDerivedEntityTypeExactName(this.ActualName, entityTypeName);
            return new EntitySet(actualName, this, _metadata);
        }

        public IList<string> GetKeyNames()
        {
            return _metadata.GetDeclaredKeyPropertyNames(this.ActualName).ToList();
        }
    }
}