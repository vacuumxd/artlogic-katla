import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HiveService } from '../services/hive.service';
import { Hive } from '../models/hive';

@Component({
  selector: 'app-hive-form',
  templateUrl: './hive-form.component.html',
  styleUrls: ['./hive-form.component.css']
})
export class HiveFormComponent implements OnInit {

  hive = new Hive(0, "", "", "", false, "");
  existed = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private hiveService: HiveService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      if (p['id'] === undefined) return;
      this.hiveService.getHive(p['id']).subscribe(h => this.hive = h);
      this.existed = true;
    });
  }

  navigateToHives() {
    this.router.navigate(['/hives']);
  }

  onCancel() {
    this.navigateToHives();
  }
  
  onSubmit() {
    if (this.existed) {
      this.hiveService.updateHive(this.hive).subscribe(() => this.navigateToHives());
    } else {
      this.hiveService.addHive(this.hive).subscribe(() => this.navigateToHives());
    }
  }

  onDelete() {
    if (confirm("Are you sure you want to delete this hive?")) {
      this.hiveService.deleteHive(this.hive.id).subscribe(() => this.navigateToHives());
    }
  }

  onUndelete() {
    this.hiveService.setHiveStatus(this.hive.id, false).subscribe(() => this.navigateToHives());
  }

  onPurge() {
    if (confirm("Are you sure you want to purge this hive?")) {
      this.hiveService.setHiveStatus(this.hive.id, true).subscribe(() => this.navigateToHives());
    }
  }
}
