-- Script cập nhật hình ảnh sản phẩm với link thật từ các nguồn công khai
USE ShoppingCart;
GO

-- Cập nhật hình ảnh cho Laptops
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1517336714731-489689fd1ca8?w=800&q=80' WHERE Slug = 'macbook-pro-16-m3';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1611186871348-b1ce696e52c9?w=800&q=80' WHERE Slug = 'macbook-air-m2';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1593642632823-8f785ba67e45?w=800&q=80' WHERE Slug = 'dell-xps-15';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1496181133206-80ce9b88a853?w=800&q=80' WHERE Slug = 'hp-spectre-x360';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1603302576837-37561b2e2302?w=800&q=80' WHERE Slug = 'asus-rog-strix-g16';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1588872657578-7efd1f1555ed?w=800&q=80' WHERE Slug = 'lenovo-thinkpad-x1';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1484788984921-03950022c9ef?w=800&q=80' WHERE Slug = 'dell-inspiron-15';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1525547719571-a2d4ac8945e2?w=800&q=80' WHERE Slug = 'hp-pavilion-14';

-- Cập nhật hình ảnh cho Smartphones
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1695048064481-d9scaled2e7d?w=800&q=80' WHERE Slug = 'iphone-15-pro-max';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1592286927505-b6e1b01a2c1f?w=800&q=80' WHERE Slug = 'iphone-15';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1610945415295-d9bbf067e59c?w=800&q=80' WHERE Slug = 'samsung-s24-ultra';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=800&q=80' WHERE Slug = 'samsung-s24';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1598327105666-5b89351aff97?w=800&q=80' WHERE Slug = 'xiaomi-14-ultra';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1585060544812-6b45742d762f?w=800&q=80' WHERE Slug = 'redmi-note-13-pro';

-- Cập nhật hình ảnh cho Tablets
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0?w=800&q=80' WHERE Slug = 'ipad-pro-129-m2';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1585790050230-5dd28404f1e9?w=800&q=80' WHERE Slug = 'ipad-air-m2';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1561154464-82e9adf32764?w=800&q=80' WHERE Slug = 'galaxy-tab-s9-ultra';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1542751110-97427bbecf20?w=800&q=80' WHERE Slug = 'galaxy-tab-a9-plus';

-- Cập nhật hình ảnh cho Headphones
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1606841837239-c5a1a4a07af7?w=800&q=80' WHERE Slug = 'airpods-pro-2';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1625159057820-f79ead2c2d3c?w=800&q=80' WHERE Slug = 'airpods-max';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1545127398-14699f92334b?w=800&q=80' WHERE Slug = 'sony-wh-1000xm5';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1590658268037-6bf12165a8df?w=800&q=80' WHERE Slug = 'galaxy-buds2-pro';

-- Cập nhật hình ảnh cho Cameras
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1606510907744-c3f2796c8c28?w=800&q=80' WHERE Slug = 'sony-a7-iv';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1502920917128-1aa500764cbd?w=800&q=80' WHERE Slug = 'canon-r6-mark-ii';

-- Cập nhật hình ảnh cho Watches
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1434493789847-2f02dc6ca35d?w=800&q=80' WHERE Slug = 'apple-watch-series-9';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1579586337278-3befd40fd17a?w=800&q=80' WHERE Slug = 'apple-watch-ultra-2';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1508685096489-7aacd43bd3b1?w=800&q=80' WHERE Slug = 'galaxy-watch-6';

-- Cập nhật hình ảnh cho Speakers
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?w=800&q=80' WHERE Slug = 'homepod-2';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1545454675-3531b543be5d?w=800&q=80' WHERE Slug = 'sony-srs-xb43';

-- Cập nhật hình ảnh cho Mouse
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1527814050087-3793815479db?w=800&q=80' WHERE Slug = 'logitech-mx-master-3s';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1615663245857-ac93bb7c39e7?w=800&q=80' WHERE Slug = 'logitech-g502-x';

-- Cập nhật hình ảnh cho Keyboards
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=800&q=80' WHERE Slug = 'logitech-mx-keys-s';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1541140532154-b024d705b90a?w=800&q=80' WHERE Slug = 'logitech-g915-tkl';

-- Cập nhật hình ảnh cho Monitors
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?w=800&q=80' WHERE Slug = 'dell-u2723de';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1593640495253-23196b27a87f?w=800&q=80' WHERE Slug = 'lg-27gn950';
UPDATE Products SET Image = 'https://images.unsplash.com/photo-1616763355548-1b606f439f86?w=800&q=80' WHERE Slug = 'samsung-odyssey-g7';

SELECT COUNT(*) as 'Số sản phẩm đã cập nhật' FROM Products WHERE Image LIKE 'https://images.unsplash.com%';
GO
