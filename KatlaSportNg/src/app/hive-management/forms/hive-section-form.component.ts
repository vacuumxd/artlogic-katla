import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HiveSectionService } from '../services/hive-section.service';

@Component({
  selector: 'app-hive-section-form',
  templateUrl: './hive-section-form.component.html',
  styleUrls: ['./hive-section-form.component.css']
})
export class HiveSectionFormComponent implements OnInit {
  hiveSectionForm: FormGroup;
  isExistingSection: boolean; // Флаг для определения редактирования существующей секции

  constructor(private formBuilder: FormBuilder, private hiveSectionService: HiveSectionService) {}

  ngOnInit(): void {
    // Инициализация формы
    this.hiveSectionForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      // Другие поля формы могут быть добавлены здесь
      isDeleted: [false] // Флаг для отслеживания удаления секции
    });

    // Проверка, является ли текущая секция существующей или новой
    this.isExistingSection = /* Логика для определения существующей секции */ true; // Замените на свою логику
  }

  // Геттеры для удобства доступа к полям формы
  get name() { return this.hiveSectionForm.get('name'); }

  // Другие геттеры могут быть добавлены здесь

  onSubmit(): void {
    // Логика для отправки данных формы на сервер
    // Вызов соответствующего метода сервиса
  }

  onBack(): void {
    // Логика для возврата на предыдущую страницу
  }

  onDelete(): void {
    // Логика для удаления секции улий
  }

  onUndelete(): void {
    // Логика для восстановления удаленной секции улий
  }

  onPurge(): void {
    // Логика для окончательного удаления секции улий из базы данных
  }
}
