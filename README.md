# shopping-cart-kata

## calistirma
### docker
- src/ShoppingCart.API/ altindaki `IMAGE_TAG=VERMEK_ISTEDIGINIZ_TAG ./build.sh` komutunu calistirin. IMAGE_TAG gecmezseniz default 1 atanir.
build.sh calistirken hata alirsaniz line ending'lerin kontrol edin. calistirdiginiz komut size dockerize edilmis imaji hazirlayacaktir. cikan imaj cart-api:VERMEK_ISTEDIGINIZ_TAG seklinde olacaktir. daha sonra `docker run -p VERMEK_ISTEDIGINIZ_PORT:80 cart-api:VERMEK_ISTEDIGINIZ_TAG` seklinde uygulamayi ayaga kaldirabilirsiniz. verdiginiz port uzerinden swagger arayuzune erismek icin: http://IP_ADRESINIZ:VERMEK_ISTEDIGINIZ_PORT/swagger/index.html linkini tiklayin. buradan api ile konusabilirsiniz.
- docker image'larda cart print testlerinin flaky olmamasi icin TR culture kullanilmistir.

### windows
- vs 2019 ile uygulamayi ayaga kaldirabilirsiniz. uygulama ayaga kalktiginda http://localhost:5000/swagger/index.html adresinden api ile konusabilirsiniz.

## api endpoint'leri aciklama
- campaign, cart, category, coupon ve product resource'larinin her biri icin get all, get by id ve post endpoint'leri mevcuttur. bu endpoint'ler sirasiyla tum resource'lari doner, id ile ilgili resource'u doner ve resource'u yaratir. endpoint'lerde diger resource'larin id'lerine ihtiyac olabilir. ornegin product yaratirken category id gerekmektedir. bi resource yarattiginizda response'dan ya da get all endpoint'lerini kullanarak id'lere erisebilirsiniz.
- cart resource'u icin product eklemek, coupon uygulamak ve cart'i y azdirmak icin uc ayri endpoint daha vardir. product eklemek icin product id'ye , coupon uygulamak icin de coupon id'ye ihtiyaciniz vardir. cart'i yazdirmak icin cart/id/{id}/print endpoint'ini kullanabilirsiniz.
- CostPerDelivery ve CostPerProduct konfigurasyonlari appsettings.json uzerinden gecildi.

### ornek akis
asagidaki adimlar ornek amaclidir. bu adimlar disinda da istediginiz gibi akislari calistabilirsiniz.

#### cart'a item ekleme
1. POST /cart endpoint'ini calistirip cart yaratin. response'da donen id cart id'nizdir.
2. POST /category endpoint'ini calistirip category yaratin. response'da donen id category id'nizdir.
3. POST /product endpoint'ini calistirip product yaratin. response'da donen id product id'nizdir. bu request'de daha once yarattiginiz category'nin id'sini kullanmamaniz gerekecektir.
4. POST /cart/id/{id}/add-item endpoint'ini  calistirip cart'a item ekleyin. bu request'te daha once yarattiginiz cart id'yi ve product id'yi kullanmaniz gerekmektedir.
5. GET /cart/id/{id} ile cart'i sorgulayabilirsiniz.
#### kampanya uygulama
1. cart'a item ekleme adimlarini takip edin
2. POST /campaign endpoint'ini calistirip campaign yaratin. response'da donen id campaign id'nizdir. bu request'de daha once yarattiginiz category id kullanmaniz gerekmektedir.
3. GET /cart/id/{id} ile cart'i sorgulayip kampanyadan etkilenip etkilenmedigini gorebilirsiniz.
#### kupon uygulama
1. cart'a item ekleme adimlarini takip edin
2. POST /coupon endpoint'ini calistirip coupon yaratin. response'da donen id coupon id'nizdir. 
3. POST /cart/id/{id}/apply-coupon endpoint'ini calistirip cart'a kupon uygulamayi deneyin. bu request'te daha once yarattiginiz kupon id'yi kullanmaniz gerekmektedir.
4. GET /cart/id/{id} ile cart'i sorgulayabilirsiniz.
#### cart yazdirma
1. cart'a item ekleme adimlarini takip edin
2. GET /cart/id/{id}/print endpoint'ini cagirin.

## varsayimlar
- yeni bir kampanya yaratildiginda bu kampanyadan etkilenecek cart'lar kontrol edilmistir.
- cart'a yeni bir item eklenir ya da kampanya uygulanirsa kupon uygulanabilme durumu degisebileceginden kupon tekrar uygalanabilir mi diye kontrol edilir. 
eger uygulanamayacak durumdaysa cart uzerindenki kupon etkisi sessizce kaldirilir (hata donmuyor).
- indirim tipi miktar olan kampanyalarda product fiyatinin indirim miktarindan yuksek olmasi beklenir. eger degilse kampanya kategorisi uyussa bile product 
kampanya uygulanamayacak olarak degerlendirilir. kampanyanin gerekli urun adedi kistasina dahil edilmez.
- cart'a daha onceden eklenen product guncellenip tekrar eklendiginde son eklenen product bilgisinin en guncel oldugu varsayilmistir.
- kupona ait indirim ilk lineitem'dan baslanip, lineitem'in kampanya sonrasi kalan fiyatini dikkate alarak kuponu indirimin tamamini lineitem'a dagitacak 
sekilde kurgulanmastir. ornegin 10 TL'lik bir kupon icin ilk line item'in indirim sonrasi kalan fiyati 15 TL ise kupon indiriminin tamami 
bu line item'a yansitilmistir. eger line item'in indirim sonrasi kalan fiyati 5 TL ise kupon'a ait 5 TL'lik indirim bu line item'a yansitilip, 
geriye kalan indirim miktari ayni mantigi takip edecek sekilde diger line item'lara sirasiyla yansitilir.

## yapilacaklar iyilestirmeler
- campaign ve cart cift tarafli konusuyor. bunu daha da basitlestirmek mumkun mu?
- error handling middleware eklenmesi
- tracing ve metric'lerin eklenmesi
- testlerde builder kullanilmasi
- validation'larda fluent validation kullanabilir

## Api Tests
- CostPerDelivery ve CostPerProduct konfigurasyonlari APITests projesinin appsettings'lerinde yapildi.