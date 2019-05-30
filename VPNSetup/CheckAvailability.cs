﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace VPNSetup
{
  public partial class CheckAvailability : Form
  {
    public CheckAvailability()
    {
      InitializeComponent();
      runSetup();
    }

    public void checkInternetConnection()
    {
      label10.Visible = true;
      label10.Text = "Checking...";
      if (InternetInfo.IsInternetAvailable)
      {
        label10.Text = "Available";
      }
      else
        label10.Text = "Unavailable";
    }

    public void checkHostedNetwork()
    {
      label11.Visible = true;
      label11.Text = "Checking...";
      command cmd = new command();
      var show_drivers = cmd.showDrivers();
      ProcessCmd process = new ProcessCmd(show_drivers);
      process.start();
      var show_output = process.getOutput().output;
      string status = String.Empty;
      string[] lines = show_output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains("Hosted network supported"))
        {
          var result_str = line.ToString();
          status = line.Split(':')[1];
          status = status.Trim();
          break;
        }
      }
      if (String.IsNullOrEmpty(status))
        label11.Text = "Unknown";
      if (status == "No")
        label11.Text = "Not Supported";
      else if (status == "Yes")
        label11.Text = "Supported";
    }

    public void checkEpressVPNAdapter()
    {
      label12.Visible = true;
      label12.Text = "Checking...";
      var adapters = NetworkAdapters.setAdapters();
      if (adapters.SharedConnection != null)
      {
        label12.Text = "Available";
      }
      else
        label12.Text = "Unavailable";
    }

    public void checkServices()
    {
      label13.Visible = true;
      label13.Text = "Checking...";
      var status = Services.startServices();
      if (status)
      {
        label13.Text = "Running";
      }
      else
        label13.Text = "Not Running";
    }

    private void pictureBox4_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void runSetup()
    {
      checkInternetConnection();
      checkHostedNetwork();
      checkEpressVPNAdapter();
      checkServices();
    }
  }
}