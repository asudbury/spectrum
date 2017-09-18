namespace Spectrum.Content.Appointments.Services
{
    using Ical.Net;
    using Ical.Net.DataTypes;
    using Ical.Net.Interfaces.DataTypes;
    using Ical.Net.Serialization;
    using Ical.Net.Serialization.iCalendar.Serializers;
    using System;
    using System.Collections.Generic;
    using System.Net.Mime;
    using Models;

    // ReSharper disable once InconsistentNaming
    public class ICalendarService : IICalendarService
    {
        /// <summary>
        /// Gets the ical appoinment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        /// <inheritdoc />
        public ICalAppointmentModel GetICalAppoinment(
            AppointmentModel model,
            string guid = null,
            int sequence = 0)
        {
            Event calendarEvent = new Event
            {
                DtStart = new CalDateTime(model.StartTime),
                DtEnd = new CalDateTime(model.EndTime),
                Description = model.Description,
                Summary = model.Description,
                Organizer = new Organizer(model.CreatedUser),
                Status = GetStatus(model.Status),
                Location = model.Location,
                Attendees = GetAttendees(model.Attendees)
            };

            if (guid != null)
            {
                calendarEvent.Uid = guid;
            }

            calendarEvent.Sequence = sequence;

            Calendar calendar = new Calendar();
            
            calendar.Events.Add(calendarEvent);

            CalendarSerializer serializer = new CalendarSerializer(new SerializationContext());
            string serializedCalendar = serializer.SerializeToString(calendar);

            //// according to the Ical spec Status should be in uppercase!
            serializedCalendar = serializedCalendar.Replace("STATUS:Confirmed", "STATUS:CONFIRMED");
            serializedCalendar = serializedCalendar.Replace("STATUS:Cancelled", "STATUS:CANCELLED");

            return new ICalAppointmentModel
            {
                Guid = calendarEvent.Uid,
                SerializedString = serializedCalendar,
                ContentType = GetContentType(model.Status),
                Sequence = calendarEvent.Sequence
            };
        }

        /// <summary>
        /// Gets the attendees.
        /// </summary>
        /// <param name="attendees">The attendees.</param>
        /// <returns></returns>
        IList<IAttendee> GetAttendees(IList<AppointmentAttendeeModel> attendees)
        {
            IList<IAttendee> attendeeList = new List<IAttendee>();

            foreach (AppointmentAttendeeModel attendee in attendees)
            {
                attendeeList.Add(new Attendee { Value = new Uri("mailto:" + attendee) });
            }

            return attendeeList;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        internal EventStatus GetStatus(int status)
        {
            return AppointmentStatus.Deleted == (AppointmentStatus)status ? 
                EventStatus.Cancelled : 
                EventStatus.Confirmed;
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <returns></returns>
        internal ContentType GetContentType(int status)
        {
            return AppointmentStatus.Deleted == (AppointmentStatus)status ? 
                new ContentType("text/calendar; method=CANCEL; charset=UTF-8;component=vevent") : 
                new ContentType("text/calendar;charset=UTF-8;component=vevent");
        }
    }
}