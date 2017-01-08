namespace Spectrum.Content.WorkFlows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Logging;
    using Umbraco.Forms.Core;
    using Umbraco.Forms.Core.Enums;
    
    public class BaseWorkFlowType : WorkflowType
    {
        /// <summary>
        /// Executes the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <param name="e">The <see cref="RecordEventArgs"/> instance containing the event data.</param>
        /// <returns>The Work Flow Execution Status.</returns>
        public override WorkflowExecutionStatus Execute(
            Record record, 
            RecordEventArgs e)
        {
            return WorkflowExecutionStatus.Completed;
        }

        /// <summary>
        /// Validates the settings.
        /// </summary>
        /// <returns>A list of exceptions.</returns>
        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }

        /// <summary>
        /// Get field value from form based on caption
        /// </summary>
        /// <param name="record">Form record</param>
        /// <param name="caption">The caption to check</param>
        /// <param name="required">True trows an Eception if field is not provided, false returns null</param>
        /// <returns>The string value from the field based on provided caption</returns>
        protected T GetFieldValue<T>(Record record, string caption, bool required)
        {
            try
            {
                if (record.RecordFields.Values.All(p => p.Field.Caption != caption))
                {
                    if (required)
                    {
                        throw new Exception("Required field is not provided!");
                    }

                    return (T)Convert.ChangeType(null, typeof(T));
                }

                string fieldValue = record.RecordFields.Values.First(p => p.Field.Caption == caption).ValuesAsString();

                return (T)Convert.ChangeType(fieldValue, typeof(T));
            }

            catch (Exception exception)
            {
                LogHelper.Error<string>(exception.Message, exception);
                throw new Exception(exception.Message);
            }
        }
    }
}
