# ImageViewer

こちらは私がポートフォリオの1つとして作成した画像ビューアです。
 
 <img width="619" alt="ImageViewer" src="https://user-images.githubusercontent.com/68487631/100676684-b6d90700-33ac-11eb-8249-85fdc8a5aac0.png">
 
# How to Use

起動時はサンプル画像を表示します。


1. "File(ファイル)" メニュー

	1. "Open(開く)" メニューアイテム

		1. フォルダ選択ダイアログが開きます。該当フォルダを選択します。
   
		1. 該当フォルダ配下にある画像ファイル(*.png, *.gif, *.jpeg, *.tiff, *.bmp)をTreeViewとして表示します。任意の画像ファイル名をクリックするとその画像を表示します。

		1. 画像をスクロールすると、表示する部分にしたがってサムネイル上にあるビューポートの大きさが変わります。ビューポートをドラッグすると連動して画像が動きます。

		1. 画像上でマウスホイールすると拡大・縮小します。

	1. "Close(終了)" メニューアイテム

		1. アプリケーションを終了します。


1. "View(表示)" メニュー

	1. "Thumnail(サムネイル)" メニューアイテム

		1. サムネイル表示有無を切り替えます。

	1. "Slideshow(スライドショー)" メニューアイテム

		1. TreeView表示時にスライドショーを開始・終了します。TreeView上でディレクトリをを選択していたらその配下の画像ファイル、画像ファイルを選択していたらそのカレントディレクトリ配下の画像ファイルを一定間隔で表示します(デフォルトでは1秒間隔です)。


1. "Option(オプションツリービュー)"

	1. "Setting(設定)" メニューアイテム

		1. 以下の設定項目を変更できます。  ・"Language(言語)" - 日本語またはEnglish  ・"ViewportColor(ビューポートの色)" - Red, Blue, Green, Yellow, Pink  ・"Slideshow Interval(スライドショーの間隔)" - 1, 5, 10, 20, 30 seconds(秒)

# Features
 
MVVM(Model-View-ViewModel)パターンを採用し、互いの疎結合性を高めています。
画面表示・画像処理はすべて添付ビヘイビアに定義しコードビハインドから切り離すことで、
保守性・再利用性の高いコードを実現しています。

Web上にはフリーの画像ビューアが数多く公開されていますが、ビューポート機能を
実装しているものは見受けられなかったため、そこが差別化点です。

# Note

開発環境 : Visual Studio 2017 Community

# Author

Anosefff
 
# License

"ImageViewer" is under [MIT license](https://en.wikipedia.org/wiki/MIT_License).

また、ディレクトリ・画像アイコンはフリー画像を使用しており、著作権は下記にあります。

https://sozai.cman.jp/

