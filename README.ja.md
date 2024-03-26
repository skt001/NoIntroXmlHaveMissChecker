# No-Intro XML Have/Miss Checker

No-Intro XML Have/Miss Checker は、ゲームコレクションを No-Intro XML DAT ファイルと比較するためのツールです。XML ファイルに提供される情報に基づいて、コレクションに存在するゲームと欠けているゲームを識別するのに役立ちます。

## 主な機能

- ゲームコレクションを No-Intro XML DAT ファイルと比較
- コレクション内の「Have」（所有）および「Miss」（欠落）ゲームの識別
- サブフォルダ内のゲームを含める再帰的フォルダスキャン
- 通常のファイルと ZIP アーカイブの両方をサポート
- 簡単なナビゲーションと比較のための直感的なユーザーインターフェース
- ゲーム名、説明、ファイル名、サイズ、CRC、MD5、SHA1 を含む詳細なゲーム情報の表示
- 組み込みの検索ボタンを使用した欠落ゲームのクイック検索機能

## 重要な注意事項

このツールは、ゲームファイルに対して CRC、MD5、または SHA1 のチェックを行わないことに注意してください。ゲームの「Have」および「Miss」ステータスを判断するために、ファイル名のみに依存しています。表示される CRC、MD5、および SHA1 の情報は純粋に情報提供を目的としており、比較には使用されません。

このツールの主な目的は、ファイル名のみに基づいて No-Intro XML DAT ファイルとゲームコレクションを比較する簡単な方法を提供することです。広範なファイル整合性チェックを必要とせずに、コレクションの完全性の概要を把握できるように設計されています。

## 始め方

1. [リリースページ](https://github.com/yourusername/No-Intro-XML-Have-Miss-Checker/releases)から No-Intro XML Have/Miss Checker の最新リリースをダウンロードします。
2. ダウンロードした ZIP ファイルを任意の場所に解凍します。
3. `No-Intro XML Have/Miss Checker.exe` ファイルを実行してアプリケーションを起動します。
4. 直感的なユーザーインターフェースを使用して、No-Intro XML DAT ファイルを読み込み、ゲームコレクションを含むフォルダを指定します。
5. 「Compare」ボタンをクリックして、比較プロセスを開始します。
6. 「Have」タブと「Miss」タブに結果が表示されます。これらのタブには、所有しているゲームと欠落しているゲームがそれぞれ表示されます。

## 貢献

No-Intro XML Have/Miss Checker への貢献を歓迎します！問題が発生した場合、改善の提案がある場合、または新機能の提供を希望する場合は、[GitHub リポジトリ](https://github.com/yourusername/No-Intro-XML-Have-Miss-Checker)で issue を開くか、pull request を送信してください。

## ライセンス

No-Intro XML Have/Miss Checker は、[MIT ライセンス](https://opensource.org/licenses/MIT)の下でリリースされています。

## 謝辞

No-Intro XML Have/Miss Checker は、ビデオゲームの ROM ダンプを保存およびドキュメント化する際の [No-Intro](https://no-intro.org) コミュニティの優れた取り組みに依存しています。このツールを可能にする XML DAT ファイルの作成と維持に尽力してくださったことに感謝します。