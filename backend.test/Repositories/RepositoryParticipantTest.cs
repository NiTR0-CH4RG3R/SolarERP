using backend.Repositories.Interfaces;
using backend.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Models.Domains;

namespace backend.test.Repositories {
	public class RepositoryParticipantTest {
		private readonly IDbConnection _connection;
		private readonly IRepositoryParticipant _repository;

		public RepositoryParticipantTest() {
			_connection = new MySqlConnection( "Server=db-mysql-blr1-08473-do-user-14661818-0.c.db.ondigitalocean.com;Database=new_erp;Uid=doadmin;Pwd=AVNS_VXDCh37l8j_R6GxtxAI;Port=25060" );
			_connection.Open();
			_repository = new RepositoryParticipant( _connection );
		}

		[Fact]
		public async void Test_CreateAsync() {
			Participant? result = null;

			try {
				result = await _repository.CreateAsync( new Participant {
					CompanyId = 1,
					FirstName = "Test First Name",
					LastName = "Test Last Name",
					Email = "",
					Phone01 = "",
					Phone02 = "",
					Address = "Test Address",
					Category = ParticipantCategory.Customer.ToString(),
					// Category = null,
					Profession = "Test Profession",
					Comments = "Test Comments",
					LastUpdatedBy = null,
					LastUpdatedDateTime = DateTime.Now
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllByCompanyAsync() {
			IEnumerable<Participant>? result = null;

			try {
				result = await _repository.GetAllByCompanyAsync( 1 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllEmlployeesByCompanyWithLimitAsync() {
			IEnumerable<Participant>? result = null;

			try {
				result = await _repository.GetAllEmlployeesByCompanyWithLimitAsync( 1, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetAllNonEmlployeesByCompanyWithLimitAsync() {
			IEnumerable<Participant>? result = null;

			try {
				result = await _repository.GetAllNonEmlployeesByCompanyWithLimitAsync( 1, 0, 10 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_GetByIdAsync() {
			Participant? result = null;

			try {
				result = await _repository.GetByIdAsync( 1 );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}

		[Fact]
		public async void Test_UpdateAsync() {
			Participant? result = null;

			try {
				result = await _repository.UpdateAsync( new Participant {
					Id = 1,
					CompanyId = 1,
					FirstName = "Test Firsasast Name",
					LastName = "Test Last Name",
					Email = "",
					Phone01 = "",
					Phone02 = "",
					Address = "Test Address",
					Category = ParticipantCategory.Customer.ToString(),
					// Category = null,
					Profession = "Test Profession",
					Comments = "Test Comments",
					LastUpdatedBy = null,
					LastUpdatedDateTime = DateTime.Now
				} );
			}
			catch ( Exception ex ) {
				Assert.Fail( ex.Message );
			}

			Assert.NotNull( result );
		}
	}
}
