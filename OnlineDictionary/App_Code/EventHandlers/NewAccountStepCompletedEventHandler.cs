using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NewCompanyStepCompletedEventHandler
/// </summary>
public class NewAccountStepCompletedEventArgs : EventArgs
{
    Guid aspnetid;

    public Guid ASPNETID
    {
        get
        {
            return aspnetid;
        }
        set
        {
            aspnetid = value;
        }
    }

    int currentAccountId;

    public int CurrentAccountId
    {
        get
        {
            return currentAccountId;
        }
        set
        {
            currentAccountId = value;
        }
    }

    int currentCountryId;

    public int CurrentCountryId
    {
        get
        {
            return currentCountryId;
        }
        set
        {
            currentCountryId = value;
        }
    }

    int setupCompleted;

    public int SetupCompleted
    {
        get
        {
            return setupCompleted;
        }
        set
        {
            setupCompleted = value;
        }
    }

    NewAccountWizardSteps currentStepCompleted;

    public NewAccountWizardSteps CurrentStepCompleted
    {
        get
        {
            return currentStepCompleted;
        }
        set
        {
            currentStepCompleted = value;
        }
    }

    public NewAccountStepCompletedEventArgs(NewAccountWizardSteps completedStep)
    {
        currentStepCompleted = completedStep;
    }

    public NewAccountStepCompletedEventArgs()
    {

    }
}

public delegate void NewAccountStepCompletedEventHandler(object sender, NewAccountStepCompletedEventArgs e);

public enum NewAccountWizardSteps
{
    AccountSecurity,
    PersonalInformation,
    AdditionalInformation,
    SetupCompleted
}