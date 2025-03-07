﻿using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations
{
	internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("users");

			builder.HasKey(users => users.Id);

			builder.Property(user => user.FirstName)
				.HasMaxLength(200)
				.HasConversion(firstname => firstname.Value, value => new FirstName(value));

			builder.Property(user => user.LastName)
				.HasMaxLength(2000)
				.HasConversion(lastname => lastname.Value, value => new LastName(value));

			builder.Property(user => user.Email)
			.HasMaxLength(2000)
			.HasConversion(email => email.Value, value => new Domain.Users.Email(value));

			builder.HasIndex(user => user.Email).IsUnique();
		}
	}
}