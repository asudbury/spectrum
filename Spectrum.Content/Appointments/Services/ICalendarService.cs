namespace Spectrum.Content.Appointments.Services
{
    using Ical.Net;
    using Ical.Net.DataTypes;
    using Ical.Net.Interfaces.DataTypes;
    using Ical.Net.Serialization;
    using Ical.Net.Serialization.iCalendar.Serializers;
    using System;
    using System.Collections.Generic;
    using Models;

    // ReSharper disable once InconsistentNaming
    public class ICalendarService : IICalendarService
    {
        /// <summary>
        /// Gets the ical appoinment.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public ICalAppointmentModel GetICalAppoinment(AppointmentModel model)
        {
            Event calendarEvent = new Event
            {
                DtStart = new CalDateTime(model.StartTime),
                DtEnd = new CalDateTime(model.EndTime),
                Description = model.Description,
                Location = model.Location//,
                ////Attendees = GetAttendees(model.Attendees)
            };

            Calendar calendar = new Calendar();
            calendar.Events.Add(calendarEvent);

            CalendarSerializer serializer = new CalendarSerializer(new SerializationContext());
            string serializedCalendar = serializer.SerializeToString(calendar);

            return new ICalAppointmentModel
            {
                Guid = calendarEvent.Uid,
                SerializedString = serializedCalendar
            };
        }

        /// <summary>
        /// Gets the attendees.
        /// </summary>
        /// <param name="attendees">The attendees.</param>
        /// <returns></returns>
        IList<IAttendee> GetAttendees(IList<string> attendees)
        {
            IList<IAttendee> attendeeList = new List<IAttendee>();

            foreach (string attendee in attendees)
            {
                attendeeList.Add(new Attendee { Value = new Uri("mailto:" + attendee) });
            }

            return attendeeList;
        }
    }
}