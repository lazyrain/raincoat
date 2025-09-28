## アクティブウィンドウ監視機能の実装計画

### 1. 概要

現在アクティブになっているウィンドウを監視し、特定のウィンドウ（例：特定のゲーム）がアクティブになった場合に、設定されたアクション（例：OBSのフィルタを有効化する）を自動的に実行する機能を実装する。

### 2. 実装ステップ

既存のアーキテクチャ（Domains, UseCases, Infrastructures）を踏襲し、以下のステップで実装を進める。

#### ステップ1: ウィンドウ情報取得サービスの作成

Windows API を利用して現在アクティブなウィンドウのタイトルを取得するためのサービスを `Domains` レイヤーに作成する。

- **作成するファイル:** `raincoat/Domains/Services/ActiveWindowService.cs`
- **実装内容:**
  - P/Invoke を使用して Win32 API の `GetForegroundWindow()` と `GetWindowText()` を呼び出す。
  - 現在アクティブなウィンドウのタイトルを返すメソッド（例: `GetActiveWindowTitle()`）を実装する。

#### ステップ2: 設定項目の追加

個々のコマンド設定に、ウィンドウ監視トリガーの項目を追加する。

- **修正するファイル:** `raincoat/Domains/Entities/KeyCommandPair.cs`
- **修正内容:**
  - `IsWindowTrigger` (bool): ウィンドウ監視を有効にするかどうかのフラグ。
  - `TriggerWindowTitle` (string): 監視対象のウィンドウタイトル（部分一致）。

#### ステップ3: ウィンドウ監視とアクション実行ユースケースの作成

バックグラウンドで定期的にアクティブウィンドウをチェックし、設定と一致した場合にアクションを実行する `UseCase` を作成する。

- **作成するファイル:** `raincoat/UseCases/Triggers/MonitorActiveWindow.cs` (および関連する Input/OutputPack)
- **実装内容:**
  - コンストラクタで `ActiveWindowService` と設定データ (`ConfigData`) を受け取る。
  - バックグラウンドで実行されるループ処理を持つ。
    - `ActiveWindowService` を使って現在のウィンドウタイトルを取得。
    - `ConfigData.KeyCommands` リストを走査し、`IsWindowTrigger` が true で、かつ `TriggerWindowTitle` が現在のウィンドウタイトルに部分一致するものを探す。
    - 条件に一致した `KeyCommandPair` の `SkillType` と `Argument` を使って、アクションを実行する。
  - この `UseCase` はアプリケーション起動時に一度だけ呼び出され、アプリケーション終了時まで動作し続ける想定。

- **現在対応中:**
  - 現在の状況は、`OBSWebSocketService`がそれを必要とするスキルに渡されていないことによるコンパイルエラーの修正中である
- テストプロジェクト (raincoat.Tests) のターゲットフレームワークが、参照しているプロジェクト (raincoat) と異なるため、テスト実行時に互換性エラーが発生している。
  - raincoat.Tests は net6.0-windows7.0 をターゲットにしているが、raincoat は net8.0-windows7.0 をターゲットにしている。
  - 解決策: raincoat.Tests のターゲットフレームワークを net8.0-windows7.0 に更新する。


#### ステップ4: UIへの統合

コマンドごとの設定画面で、ウィンドウ監視の設定を行えるようにする。

- **修正するファイル:** `raincoat/ButtonSetting.cs` および `raincoat/ButtonSetting.Designer.cs`
- **修正内容:**
  - 「アクティブウィンドウをトリガーにする」チェックボックスを追加。
  - 監視したいウィンドウのタイトルを入力するテキストボックスを追加。
  - チェックボックスの状態に応じてテキストボックスの有効/無効を切り替える。
  - `KeyCommandPair` オブジェクトの `IsWindowTrigger` と `TriggerWindowTitle` プロパティを、これらのUIコントロールに紐づけて読み書きする。

### 3. 変更が予想されるファイル一覧

- **新規作成:**
  - `raincoat/Domains/Services/ActiveWindowService.cs`
  - `raincoat/UseCases/Triggers/MonitorActiveWindow.cs`
  - `raincoat/UseCases/Triggers/MonitorActiveWindowInputPack.cs`
  - `raincoat/UseCases/Triggers/MonitorActiveWindowOutputPack.cs`
- **修正:**
  - `raincoat/Domains/Entities/KeyCommandPair.cs`
  - `raincoat/ButtonSetting.cs`
  - `raincoat/ButtonSetting.Designer.cs`
  - `raincoat/Config.cs` (監視サービスを開始するため)
  - `raincoat.sln` / `raincoat.csproj` (新規ファイル追加のため)
