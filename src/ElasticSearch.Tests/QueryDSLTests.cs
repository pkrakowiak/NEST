﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ElasticSearch.Client;
using HackerNews.Indexer.Domain;
using ElasticSearch.Client.DSL;

namespace ElasticSearch.Tests
{
	[TestFixture]
	public class QueryDSLTests : BaseElasticSearchTests
	{
		[Test]
		public void FuzzySearch()
		{
			var client = this.ConnectedClient;
			QueryResponse<Post> queryResults = client.Search<Post>(new Search()
			{
				Query = new Query(new Fuzzy("author.firstName", "kimchy", 1.0))

			}.Skip(0).Take(10)
			);
		}

	}
}
