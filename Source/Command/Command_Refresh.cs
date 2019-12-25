﻿using JsonFlier.UserControls.Logs;

namespace JsonFlier.Command
{
    public class Command_Refresh : ICommand
    {
        public Command_Refresh(JsonArray jsonArrayControl)
        {
            JsonArrayControl = jsonArrayControl;
        }

        public JsonArray JsonArrayControl { get; }

        public void Execute()
        {
            JsonArrayControl.Refresh();
        }
    }
}