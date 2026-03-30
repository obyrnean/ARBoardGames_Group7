using System.Diagnostics;
using UnityEngine;

public class StockfishManager : MonoBehaviour {
    private Process stockfishProcess;

    void Start() {
        stockfishProcess = new Process {
            StartInfo = new ProcessStartInfo {
                FileName = Application.streamingAssetsPath + "/stockfish-android-armv8",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        stockfishProcess.Start();
        stockfishProcess.StandardInput.WriteLine("uci");
        stockfishProcess.StandardInput.WriteLine("isready");
    }

    public void SendMove(string moveHistory) {
        stockfishProcess.StandardInput.WriteLine("position startpos moves " + moveHistory);
        stockfishProcess.StandardInput.WriteLine("go movetime 1000");
        string response = stockfishProcess.StandardOutput.ReadLine();
        UnityEngine.Debug.Log("Stockfish says: " + response);
    }
}
