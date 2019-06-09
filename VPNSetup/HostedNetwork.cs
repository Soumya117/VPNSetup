﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace VPNSetup
{
  class HostedNetwork
  {
    public static string ExtractHostedNetwork(string result)
    {
      string hostedNetwork = String.Empty;
      string[] lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains("SSID"))
        {
          var result_str = line.ToString();
          var ssid = line.Split(':')[1];
          hostedNetwork = string.Join(" ", ssid.Split('"').Where((x, i) => i % 2 != 0));
          break;
        }
      }
      return hostedNetwork;
    }

    public static string Start()
    {
      var start_cmd = Command.StartHostedNetwork();
      var connect_output = ProcessCmd.GetOutput(start_cmd);
      return connect_output;
    }

    public static string Stop()
    {
      var stop_cmd = Command.StopHostedNetwork();
      var stop_output = ProcessCmd.GetOutput(stop_cmd);
      return stop_output;
    }

    public static string Connect()
    {
      NetworkAdapters.EnableExpressVPNAdapter();
      var view_output = ViewHostedNetwork();
      if (String.IsNullOrEmpty(view_output))
      {
        return null;
      }
      var connect_output = Start();
      NetworkAdapters.DisableNetShare();
      NetworkAdapters.EnableNetShare();
      return connect_output;
    }

    public static string Create(string ssid, string key)
    {
      var create_cmd = Command.CreateHostedNetwork(ssid, key);
      var create_output = ProcessCmd.GetOutput(create_cmd);
      return create_output;
    }

    public static void ShowPassword()
    {
      var show_pwd = Command.ShowPassword();
      var result = ProcessCmd.GetOutput(show_pwd);
      if (String.IsNullOrEmpty(result))
      {
        return;
      }
      string[] lines = result.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (string line in lines)
      {
        if (line.Contains("User security key"))
        {
          var key = line.Split(':')[1];
          MessageBox.Show("Password is: " + key, "Info");
          return;
        }
      }
    }

    public static string ChangePassword()
    {
      var output = String.Empty;
      string password = Interaction.InputBox(
         "Enter new password",
         "Input",
         "",
         -1,
         -1);

      if (password.Length > 0)
      {
        if (password.Length < 8)
        {
          MessageBox.Show("Password must be atleast 8 characters.", "Info");
          return null;
        }

        var view_output = ViewHostedNetwork();
        var hostname = ExtractHostedNetwork(view_output);
        var create_cmd = Command.CreateHostedNetwork(hostname, password);
        output = ProcessCmd.GetOutput(create_cmd);
      }
      else
        return null;

      return output;
    }
    public static string ExtractStatus(string data)
    {
      string status = String.Empty;
      string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains("Status"))
        {
          var result_str = line.ToString();
          status = line.Split(':')[1];
          break;
        }
      }
      return status;
    }

    public static string ExtractClients(string data)
    {
      var clients = String.Empty;
      string[] lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
      foreach (var line in lines)
      {
        if (line != null && line.Contains("Number of clients"))
        {
          var result_str = line.ToString();
          clients = line.Split(':')[1];
          break;
        }
      }
      return clients;
    }

    public static string ViewHostedNetwork()
    {
      var show_cmd = Command.ViewHostedNetwork();
      var view_output = ProcessCmd.GetOutput(show_cmd);
      if (String.IsNullOrEmpty(view_output))
      {
        //TODO log
        Console.WriteLine("View hosted network returned empty..");
      }
      return view_output;
    }
  }
}