namespace Husa.Extensions.ShowingTime.Data.DbConfiguration
{
    using System;
    using Husa.Extensions.Linq;
    using Husa.Extensions.ShowingTime.Enums;
    using Husa.Extensions.ShowingTime.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class ShowingTimeExtensions
    {
        public static void ConfigureShowingTime<T>(this EntityTypeBuilder<T> builder)
            where T : class, IShowingTime
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.OwnsOne(
                p => p.AdditionalInstructions,
                b => b.ConfigureAdditionalInstructions());
            builder.OwnsOne(
                p => p.AppointmentRestrictions,
                b => b.ConfigureAppointmentRestriction());
            builder.OwnsOne(
                p => p.AccessInformation,
                b => b.ConfigureAccessInformation());
            builder.OwnsOne(
                p => p.AppointmentSettings,
                b => b.ConfigureAppointmentSettings());
        }

        public static void ConfigureShowingTimeContact<T>(this EntityTypeBuilder<T> builder)
            where T : class, IShowingTimeContact
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.Property(p => p.FirstName).HasMaxLength(100)
                .HasColumnName(nameof(IShowingTimeContact.FirstName));

            builder.Property(p => p.LastName).HasMaxLength(100)
                .HasColumnName(nameof(IShowingTimeContact.LastName));

            builder.Property(p => p.OfficePhone).HasMaxLength(15)
                .HasColumnName(nameof(IShowingTimeContact.OfficePhone));

            builder.Property(p => p.MobilePhone).HasMaxLength(15)
                .HasColumnName(nameof(IShowingTimeContact.MobilePhone));

            builder.Property(p => p.Email).HasMaxLength(100)
                .HasColumnName(nameof(IShowingTimeContact.Email));

            builder.Property(p => p.ConfirmAppointmentsByOfficePhone)
                .HasColumnName(nameof(IShowingTimeContact.ConfirmAppointmentsByOfficePhone));

            builder.Property(p => p.ConfirmAppointmentCallerByOfficePhone)
                .HasColumnName(nameof(IShowingTimeContact.ConfirmAppointmentCallerByOfficePhone))
                .HasConversion<string>()
                .HasMaxLength(25);

            builder.Property(p => p.NotifyAppointmentChangesByOfficePhone)
                .HasColumnName(nameof(IShowingTimeContact.NotifyAppointmentChangesByOfficePhone));

            builder.Property(p => p.AppointmentChangesNotificationsOptionsOfficePhone)
                .HasColumnName(nameof(IShowingTimeContact.AppointmentChangesNotificationsOptionsOfficePhone))
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(p => p.ConfirmAppointmentsByMobilePhone)
                .HasColumnName(nameof(IShowingTimeContact.ConfirmAppointmentsByMobilePhone));

            builder.Property(p => p.ConfirmAppointmentCallerByMobilePhone)
                .HasColumnName(nameof(IShowingTimeContact.ConfirmAppointmentCallerByMobilePhone))
                .HasConversion<string>()
                .HasMaxLength(25);

            builder.Property(p => p.NotifyAppointmentChangesByMobilePhone)
                .HasColumnName(nameof(IShowingTimeContact.NotifyAppointmentChangesByMobilePhone));

            builder.Property(p => p.AppointmentChangesNotificationsOptionsMobilePhone)
                .HasColumnName(nameof(IShowingTimeContact.AppointmentChangesNotificationsOptionsMobilePhone))
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(p => p.ConfirmAppointmentsByText)
                .HasColumnName(nameof(IShowingTimeContact.ConfirmAppointmentsByText));

            builder.Property(p => p.NotifyAppointmentsChangesByText)
                .HasColumnName(nameof(IShowingTimeContact.NotifyAppointmentsChangesByText));

            builder.Property(p => p.SendOnFYIByText)
                .HasColumnName(nameof(IShowingTimeContact.SendOnFYIByText));

            builder.Property(p => p.ConfirmAppointmentsByEmail)
                .HasColumnName(nameof(IShowingTimeContact.ConfirmAppointmentsByEmail));

            builder.Property(p => p.NotifyAppointmentChangesByEmail)
                .HasColumnName(nameof(IShowingTimeContact.NotifyAppointmentChangesByEmail));

            builder.Property(p => p.SendOnFYIByEmail)
                .HasColumnName(nameof(IShowingTimeContact.SendOnFYIByEmail));
        }

        public static void ConfigureAdditionalInstructions<TOwnerEntity, TDependentEntity>(this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder)
            where TOwnerEntity : class, IShowingTime
            where TDependentEntity : class, IAdditionalInstructions
        {
            builder.Property(p => p.NotesForApptStaff)
                .HasColumnName(nameof(IAdditionalInstructions.NotesForApptStaff))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(1000);
            builder.Property(p => p.NotesForShowingAgent)
                .HasColumnName(nameof(IAdditionalInstructions.NotesForShowingAgent))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(1000);
        }

        public static void ConfigureAppointmentRestriction<TOwnerEntity, TDependentEntity>(this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder)
            where TOwnerEntity : class, IShowingTime
            where TDependentEntity : class, IAppointmentRestrictions
        {
            builder.Property(p => p.AllowRealtimeAvailabilityForBrokers)
                .HasColumnName(nameof(IAppointmentRestrictions.AllowRealtimeAvailabilityForBrokers))
                .HasColumnType("bit");
            builder.Property(p => p.AllowInspectionsAndWalkThroughs)
                .HasColumnName(nameof(IAppointmentRestrictions.AllowInspectionsAndWalkThroughs))
                .HasColumnType("bit");
            builder.Property(p => p.AllowAppraisals)
                .HasColumnName(nameof(IAppointmentRestrictions.AllowAppraisals))
                .HasColumnType("bit");
            builder.Property(p => p.RequiredTimeHours)
                .HasColumnName(nameof(IAppointmentRestrictions.RequiredTimeHours))
                .HasEnumFieldValue<AppointmentTimeHours>(10);
            builder.Property(p => p.SuggestedTimeHours)
                .HasColumnName(nameof(IAppointmentRestrictions.SuggestedTimeHours))
                .HasEnumFieldValue<AppointmentTimeHours>(10);
            builder.Property(p => p.MinShowingWindowShowings)
                .HasColumnName(nameof(IAppointmentRestrictions.MinShowingWindowShowings))
                .HasEnumFieldValue<AppointmentLength>(10);
            builder.Property(p => p.MaxShowingWindowShowings)
                .HasColumnName(nameof(IAppointmentRestrictions.MaxShowingWindowShowings))
                .HasEnumFieldValue<AppointmentLength>(10);
            builder.Property(p => p.BufferTimeBetweenAppointments)
                .HasColumnName(nameof(IAppointmentRestrictions.BufferTimeBetweenAppointments))
                .HasEnumFieldValue<TimeBetweenAppointments>(10);
            builder.Property(p => p.OverlappingAppointmentMode)
                .HasColumnName(nameof(IAppointmentRestrictions.OverlappingAppointmentMode))
                .HasConversion<string>()
                .HasMaxLength(50);
            builder.Property(p => p.AdvancedNotice)
                .HasColumnName(nameof(IAppointmentRestrictions.AdvancedNotice))
                .HasConversion<string>()
                .HasMaxLength(50);
        }

        public static void ConfigureAccessInformation<TOwnerEntity, TDependentEntity>(this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder)
            where TOwnerEntity : class, IShowingTime
            where TDependentEntity : class, IAccessInformation
        {
            builder.Property(p => p.GateCode)
                .HasColumnName(nameof(IAccessInformation.GateCode))
                .HasMaxLength(100);
            builder.Property(p => p.AccessMethod)
                .HasColumnName(nameof(IAccessInformation.AccessMethod))
                .HasConversion<string>()
                .HasMaxLength(50);
            builder.Property(p => p.Location)
                .HasColumnName(nameof(IAccessInformation.Location))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(1000);
            builder.Property(p => p.Serial)
                .HasColumnName(nameof(IAccessInformation.Serial))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.Combination)
                .HasColumnName(nameof(IAccessInformation.Combination))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.SharingCode)
                .HasColumnName(nameof(IAccessInformation.SharingCode))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.CbsCode)
                .HasColumnName(nameof(IAccessInformation.CbsCode))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.Code)
                .HasColumnName(nameof(IAccessInformation.Code))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.DeviceId)
                .HasColumnName(nameof(IAccessInformation.DeviceId))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.AccessNotes)
                .HasColumnName(nameof(IAccessInformation.AccessNotes))
                .HasMaxLength(1000);
            builder.Property(p => p.ProvideAlarmDetails)
                .HasColumnName(nameof(IAccessInformation.ProvideAlarmDetails));
            builder.Property(p => p.HasManageKeySets)
                .HasColumnName(nameof(IAccessInformation.HasManageKeySets));
            builder.Property(p => p.AlarmDisarmCode)
                .HasColumnName(nameof(IAccessInformation.AlarmDisarmCode))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.AlarmArmCode)
                .HasColumnName(nameof(IAccessInformation.AlarmArmCode))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.AlarmPasscode)
                .HasColumnName(nameof(IAccessInformation.AlarmPasscode))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(100);
            builder.Property(p => p.AlarmNotes)
                .HasColumnName(nameof(IAccessInformation.AlarmNotes))
                .HasDefaultValue(string.Empty)
                .HasMaxLength(1000);
        }

        public static void ConfigureAppointmentSettings<TOwnerEntity, TDependentEntity>(this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder)
            where TOwnerEntity : class, IShowingTime
            where TDependentEntity : class, IAppointmentSettings
        {
            builder.Property(p => p.IsAgentAccompaniedShowing)
                .HasColumnName(nameof(IAppointmentSettings.IsAgentAccompaniedShowing))
                .HasColumnType("bit");
            builder.Property(p => p.IsFeedbackRequested)
                .HasColumnName(nameof(IAppointmentSettings.IsFeedbackRequested))
                .HasColumnType("bit");
            builder.Property(p => p.IsPropertyOccupied)
                .HasColumnName(nameof(IAppointmentSettings.IsPropertyOccupied))
                .HasColumnType("bit");
            builder.Property(p => p.AllowApptCenterTakeAppts)
                .HasColumnName(nameof(IAppointmentSettings.AllowApptCenterTakeAppts))
                .HasColumnType("bit");
            builder.Property(p => p.AllowShowingAgentsToRequest)
                .HasColumnName(nameof(IAppointmentSettings.AllowShowingAgentsToRequest))
                .HasColumnType("bit");
            builder.Property(p => p.AppointmentType)
                .HasColumnName(nameof(IAppointmentSettings.AppointmentType))
                .HasConversion<string>().HasMaxLength(50);
            builder.Property(p => p.FeedbackTemplate).HasConversion<string>()
                .HasColumnName(nameof(IAppointmentSettings.FeedbackTemplate))
                .HasMaxLength(30);
            builder.Property(p => p.RequiredStaffLanguage)
                .HasColumnName(nameof(IAppointmentSettings.RequiredStaffLanguage))
                .HasConversion<string>().HasMaxLength(30);
            builder.Property(p => p.AppointmentPresentationType)
                .HasColumnName(nameof(IAppointmentSettings.AppointmentPresentationType))
                .HasConversion<string>().HasMaxLength(30);
        }
    }
}
