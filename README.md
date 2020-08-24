# ImageViewer

こちらは私がポートフォリオの1つとして作成した画像ビューアです。
 
 <img width="545" alt="ImageViewer" src="https://user-images.githubusercontent.com/68487631/91004278-b3e0b480-e60e-11ea-9386-5d0b6bf78d32.png">
 
# How to Use

1. "Open Image" をクリックすると画像ファイル選択ダイアログが開きます。
   表示したい画像を選択します(デフォルトではサンプル画像を表示しています)。
   
2. 画像をスクロールすると、サムネイル上にあるViewportが連動して動きます。

3. 画像上でマウスホイールすると拡大・縮小することができます。

※ " Dispay Viewport" チェックボックスでViewport表示有無を切り替えることができます。

※ "Viewport Color" でViewportの色を変えることができます。

# Features
 
MVVM(Model-View-ViewModel)パターンを採用しています。
画像加工処理はすべて添付ビヘイビアに定義してコードビハインドから切り離すことで、
再利用性の高いコードを実現しています。

画像ビューアとしての機能はシンプルになるように心がけました。

# Note

開発環境 : Visual Studio 2017 Community

# Author

Sparks20
 
# License
 
"ImageViewer" is under [MIT license](https://en.wikipedia.org/wiki/MIT_License).

