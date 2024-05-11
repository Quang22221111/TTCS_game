if __name__ == '__main__':
    with open('D:/1/DeathtrapDungeon/Assets/_Scripts/UI/UIManager.cs', 'r', encoding='utf-8') as file:
        lines = file.readlines()  # Đọc tất cả các dòng từ file vào một danh sách
    with open('D:/1/DeathtrapDungeon/Assets/_Scripts/UI/UIManager.cs', 'w', encoding='utf-8') as file:
        for line in lines:
            # Kiểm tra xem dòng có chứa comment không
            if '//' in line:
                # Loại bỏ phần comment bằng cách cắt từ vị trí của dấu "//"
                line = line[:line.index('//')]
            file.write(line)
